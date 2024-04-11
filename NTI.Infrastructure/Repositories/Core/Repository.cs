using System;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NTI.Application.Interfaces.Repositories.Core;
using NTI.Application.OperationResultDtos;
using NTI.Domain.Models.Core;
using NTI.Infrastructure.Context;
using NTI.Application.Extensions;
using NTI.Application.InputModels.Core;
using System.Net;

namespace NTI.Infrastructure.Repositories.Core
{
    public class Repository<TEntity, TDto, TInputModel> : IRepository<TEntity, TDto, TInputModel>
    where TEntity : BaseModel
    where TDto : class
    where TInputModel : class, IIdAble
    {

        public delegate Task<TResult> TransactionAction<TResult>(IDbContextTransaction transaction) where TResult : class;
        public delegate TResult ErrorAction<TResult>(Exception exception) where TResult : class;
        private readonly IMapper _mapper;
        private readonly ProjectDbContext _context;
        private readonly DbSet<TEntity> _dbSet;


        public Repository(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Adds the entity to the Tracking System
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Add(TInputModel entity)
        {
            var mappedToEntity = _mapper.Map<TEntity>(entity);
            var dbEntity = _context.Add(mappedToEntity).Entity;
            return dbEntity;
        }
        /// <summary>
        /// Asynchrounously Adds the entity to the Tracking System
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TInputModel entity)
        {
            var mappedToEntity = _mapper.Map<TEntity>(entity);
            var dbEntity = await _context.AddAsync(mappedToEntity);
            return dbEntity.Entity;
        }

        /// <summary>
        /// Returns a Queryable of the Entity
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        /// <summary>
        /// Returns a Dto of the Entity with the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationResult<TDto>> GetByIdAsync(int id)
        {
            var result = OperationResult<TDto>.Failed();
            var mapped = await _dbSet.Where(x => x.Id == id).ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (mapped is not null)
            {
                result.SetSucceeded(mapped);
                return result;
            }
            result.SetCode(404);
            result.SetStatusCode(HttpStatusCode.NotFound);
            return result.AddError($"No Entity Was Found With Id: {id}");
        }

        /// <summary>
        /// Returns a List of Dtos for a given entity for given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<OperationResult<IEnumerable<TDto>>> GetListWithConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = OperationResult<IEnumerable<TDto>>.Failed();

            var mapped = await _dbSet.Where(predicate).ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
            if (mapped is not null)
            {
                result.SetSucceeded(mapped);
                return result;
            }
            return result.AddError("No Entity Was Found With The Given Condition");
        }

        /// <summary>
        /// Returns a List of Dtos for a given entity
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<IEnumerable<TDto>>> GetAllAsync()
        {
            var result = OperationResult<IEnumerable<TDto>>.Failed();
            var mapped = await _dbSet.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
            result.SetSucceeded(mapped);
            if (!mapped.Any())
            {
                result.SetCode(204);
                result.SetStatusCode(HttpStatusCode.NoContent);
            }
            return result;

        }

        /// <summary>
        /// Returns a Paginated List of elements
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<OperationResult<Paginated<TDto>>> GetPaged(int page = 1, int pageSize = 20)
        {
            var requestResult = OperationResult<Paginated<TDto>>.Failed();

            var paginated = new Paginated<TDto>
            {
                CurrentPage = page,
                PageSize = pageSize
            };

            var query = _dbSet.AsQueryable();

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);


            var items = await query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            paginated.TotalItems = totalItems;
            paginated.Items = items;
            paginated.TotalPages = totalPages;
            requestResult.SetSucceeded(paginated);
            return requestResult;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<OperationResult> CommitAsync(IDbContextTransaction? transaction = null, bool automaticRollback = false, Action<Exception>? onException = null)
        {
            var opResult = OperationResult.Failed();
            try
            {
                var savedRegistries = await _context.SaveChangesAsync();
                bool succeeded = savedRegistries > 0;
                opResult.Succeeded = succeeded;
                if (!succeeded)
                {
                    opResult.SetCode(500);
                    opResult.AddError("Fatal error with database while doing operation.");
                    if (transaction != null && automaticRollback) transaction.Rollback();
                }
                opResult.SetCode(200);
                return opResult;
            }
            catch (Exception e)
            {
                opResult.SetCode(500);
                onException?.Invoke(e);
                if (transaction != null && automaticRollback) transaction.Rollback();
                return opResult.AddError(e.GetError());
            }
        }

        public async Task<OperationResult<TDto>> CreateAsync(TInputModel entity, Action? onRollBack = null)
        {
            var opResult = OperationResult<TDto>.Failed();

            var dbEntity = Add(entity);
            var result = await CommitAsync();
            if (result.Succeeded)
            {
                var mapped = _mapper.Map<TDto>(dbEntity);
                opResult.SetSucceeded(mapped);
                return opResult;
            }
            else
            {
                onRollBack?.Invoke();
                opResult.SetFrom(result);
                return opResult.AddError("Error while saving the entity");
            }
        }

        public async Task<OperationResult<TDto>> EditAsync(int id, TInputModel model, Action? onRollBack = null)
        {
            var opResult = OperationResult<TDto>.Failed();

            var savedEntity = await _dbSet.FindAsync(id);
            if (savedEntity is null) return opResult;
            TEntity entityEditted = UpdateEntity(model, savedEntity);

            var result = await CommitAsync();
            if (!result.Succeeded)
            {
                onRollBack?.Invoke();
                return opResult.SetFrom(result);
            }
            var mapped = _mapper.Map<TDto>(entityEditted);
            return OperationResult<TDto>.Success(mapped);
        }

        /// <summary>
        /// Asynchrounously Soft delete an entity with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationResult> SoftDeleteByIdAsync(int id)
        {
            var result = OperationResult.Failed();
            var entity = await _dbSet.FindAsync(id);
            var type = entity!.GetType();
            var prop = type.GetProperty("IsDeleted");
            prop?.SetValue(entity, true);
            _dbSet.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
            return await CommitAsync();
        }


        private IDbContextTransaction? GetCurrentTransaction()
        {
            return _context.Database.CurrentTransaction;
        }

        private TEntity UpdateEntity(TInputModel entity, TEntity savedEntity)
        {
            _dbSet.Entry(savedEntity).CurrentValues.SetValues(entity);
            _dbSet.Entry(savedEntity).State = EntityState.Modified;
            return savedEntity;
        }

    }
}

