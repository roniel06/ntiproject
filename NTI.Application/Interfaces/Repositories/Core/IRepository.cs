using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;


namespace NTI.Application.Interfaces.Repositories.Core
{
    public interface IRepository<TEntity, TDto, TInputModel>
        where TEntity : class
        where TDto : class
        where TInputModel : class
    {
        TEntity Add(TInputModel entity);
        Task<TEntity> AddAsync(TInputModel entity);

        IQueryable<TEntity> GetQueryable();
        Task<OperationResult<TDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<TDto>>> GetAllAsync();
        Task<OperationResult<IEnumerable<TDto>>> GetListWithConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<OperationResult<Paginated<TDto>>> GetPaged(int page = 1, int pageSize = 20);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<OperationResult> CommitAsync(IDbContextTransaction? transaction = null, bool automaticRollback = false,
    Action<Exception>? onException = null);

        Task<OperationResult<TDto>> CreateAsync(TInputModel entity,
            Action? onRollBack = null, IDbContextTransaction? reuseTransaction = null, bool useContextTransaction = false);

        Task<OperationResult<TDto>> EditAsync(int id, TInputModel model,
            Action? onRollBack = null, IDbContextTransaction? reuseTransaction = null, bool useContextTransaction = false);

        Task<OperationResult> SoftDeleteByIdAsync(int id);
    }
}

