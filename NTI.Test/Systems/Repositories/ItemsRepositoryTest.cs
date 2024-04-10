using NTI.Infrastructure.Repositories;
using NTI.Infrastructure.Context;
using NTI.Test.Helpers;
using AutoFixture;
using NTI.Application.InputModels.Items;
using AutoMapper;
using FluentAssertions;
using NTI.Application.OperationResultDtos;
using NTI.Application.Dtos;
using NTI.Application.Mappings.Items;

namespace NTI.Test.Systems.Repositories
{
    public class ItemsRepositoryTest : IDisposable
    {
        private readonly ItemsRepository _sut;
        private readonly ProjectDbContext _dbContext;
        private readonly IMapper _mapper;


        public ItemsRepositoryTest()
        {
            _dbContext = TestDbContextFactory.Create();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ItemMapping>();
            });

            _mapper = config.CreateMapper();
            _sut = new ItemsRepository(_dbContext, _mapper);


        }
        public void Dispose()
        {
            TestDbContextFactory.Destroy(_dbContext);
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessWithCreatedEntityDto()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>().With(x => x.Id, 0).Create();


            // Act
            var result = await _sut.CreateAsync(item);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<ItemDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Description.Should().Be(item.Description);
            result.Payload.DefaultPrice.Should().Be(item.DefaultPrice);
            result.Payload.IsActive.Should().Be(item.IsActive);
            result.Payload.Category.Should().Be(item.Category);
        }

        [Fact]

        public async Task EditAsync_ShouldCreateAndEditCreatedRecord()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>()
                .With(x => x.DefaultPrice, 20)
                .With(x => x.Description, "Initial Description")
                .With(x => x.IsActive, true)
            .Create();

            // Act
            await _sut.AddAsync(item);
            var result = await _sut.CommitAsync();


            item.Description = "Edited Description";
            item.DefaultPrice = 30;
            item.IsActive = false;
            var editedResult = await _sut.EditAsync(item.Id ?? 0, item);

            // Assert
            editedResult.Should().NotBeNull();
            editedResult.Should().BeOfType<OperationResult<ItemDto>>();
            editedResult.IsSuccessfulWithNoErrors.Should().BeTrue();

            editedResult.Payload.Description.Should().Be(item.Description);
            editedResult.Payload.DefaultPrice.Should().Be(item.DefaultPrice);
            editedResult.Payload.IsActive.Should().Be(false);
        }


        [Fact]
        public async Task DeleteAsync_ShouldCreateAndDeleteRecord()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>()
                .With(x => x.DefaultPrice, 20)
                .With(x => x.Description, "Initial Description")
                .With(x => x.IsActive, true)
            .Create();

            // Act
            await _sut.AddAsync(item);
            await _sut.CommitAsync();

            var created = await _sut.GetByIdAsync(item.Id ?? 0);
            var deletedResult = await _sut.SoftDeleteByIdAsync(created.Payload.Id);

            // Assert
            deletedResult.Should().NotBeNull();
            deletedResult.Should().BeOfType<OperationResult?>();
            deletedResult.IsSuccessfulWithNoErrors.Should().BeTrue();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllCreatedRecords()
        {
            // Arrange 
            var fixture = new Fixture();
            var items = fixture.Build<ItemInputModel>()
            .CreateMany(10);

            // Act
            foreach (var item in items)
            {
                await _sut.AddAsync(item);
                await _sut.CommitAsync();
            }

            var allItems = await _sut.GetAllAsync();

            // Assert
            //TrueForAll(x => x.Result.IsSuccessfulWithNoErrors).Should().BeTrue();
            allItems.Should().NotBeNull();
            allItems.Should().BeOfType<OperationResult<IEnumerable<ItemDto>>>();
            allItems.Payload.Count().Should().BeGreaterThanOrEqualTo(10);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCreatedRecord()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>()
            .Create();

            // Act
            await _sut.AddAsync(item);
            await _sut.CommitAsync();

            var created = await _sut.GetByIdAsync(item.Id ?? 0);

            // Assert
            created.Should().NotBeNull();
            created.Should().BeOfType<OperationResult<ItemDto>>();
            created.IsSuccessfulWithNoErrors.Should().BeTrue();
            created.Payload.Id.Should().Be(item.Id);
        }



    }
}