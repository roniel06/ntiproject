using NTI.Infrastructure.Repositories;
using NTI.Infrastructure.Context;
using NTI.Test.Helpers;
using AutoFixture;
using NTI.Application.InputModels.Items;
using Moq;
using AutoMapper;

namespace NTI.Test.Systems.Repositories
{
    public class ItemsRepositoryTest
    {
        private readonly ItemsRepository _sut;
        private readonly ProjectDbContext _dbContext;
        Mock<IMapper> _mapperMock = new Mock<IMapper>();


        public ItemsRepositoryTest()
        {
            _dbContext = TestDbContextFactory.Create();
            _sut = new ItemsRepository(_dbContext, _mapperMock.Object);


        }
        public void Dispose()
        {
            TestDbContextFactory.Destroy(_dbContext);
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedEntity()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>().With(x => x.Id, 0).Create();

            // Act

            var result = await _sut.CreateAsync(item);

            // Assert
            Assert.NotNull(result);

        }


    }
}