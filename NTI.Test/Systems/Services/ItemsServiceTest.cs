
using AutoFixture;
using Moq;
using NTI.Application.InputModels.Items;
using NTI.Application.Services;
using AutoMapper;
using NTI.Application.Mappings.Items;
using FluentAssertions;
using NTI.Application.OperationResultDtos;
using NTI.Application.Dtos;
using NTI.Application.Interfaces.Repositories;

namespace NTI.Test.Systems.Services
{
    public class ItemsServiceTest
    {

        private readonly ItemsService _sut;
        private readonly Mock<IItemRepository> _itemsRepositoryMock = new Mock<IItemRepository>();
        private readonly IMapper _mapper;


        public ItemsServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ItemMapping>();
            });
            _mapper = config.CreateMapper();
            _sut = new ItemsService(_itemsRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnSuccessWithAllItems()
        {
            // Arrange
            var fixture = new Fixture();
            var items = fixture.CreateMany<ItemDto>(3);
            _itemsRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(OperationResult<IEnumerable<ItemDto>>.Success(items));

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<IEnumerable<ItemDto>>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Count().Should().Be(3);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessWithCreatedEntityDto()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>().With(x => x.Id, 0).Create();
            var itemDto = _mapper.Map<ItemDto>(item);

            _itemsRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<ItemInputModel>(), null))
                .ReturnsAsync(OperationResult<ItemDto>.Success(itemDto));

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
        public async Task EditAsync_ShouldReturnSuccessWithEditedEntityDto()
        {
            // Arrange 
            var fixture = new Fixture();
            int id = fixture.Create<int>();
            var item = fixture.Build<ItemInputModel>()
            .With(x => x.Id, id)
                .With(x => x.DefaultPrice, 20)
                .With(x => x.Description, "Initial Description")
                .With(x => x.IsActive, true)
                .Create();
            var itemDto = _mapper.Map<ItemDto>(item);

            _itemsRepositoryMock.Setup(x => x.EditAsync(id, It.IsAny<ItemInputModel>(), null))
                .ReturnsAsync(OperationResult<ItemDto>.Success(itemDto));

            // Act
            var result = await _sut.UpdateAsync(id, item);

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
        public async Task GetByItemNumberAsync_ShouldReturnSuccessWithItemDto()
        {
            // Arrange 
            var fixture = new Fixture();
            var itemNumber = fixture.Create<int>();
            var item = fixture.Build<ItemDto>().With(x => x.ItemNumber, itemNumber).Create();

            _itemsRepositoryMock.Setup(x => x.GetItemByItemNumber(itemNumber))
                .ReturnsAsync(item);

            // Act
            var result = await _sut.GetByItemNumberAsync(itemNumber);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<ItemDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.ItemNumber.Should().Be(itemNumber);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessWithDeletedEntityDto()
        {
            // Arrange 
            var fixture = new Fixture();
            var id = fixture.Create<int>();
            var item = fixture.Build<ItemDto>().With(x => x.Id, id).Create();

            _itemsRepositoryMock.Setup(x => x.SoftDeleteByIdAsync(id))
                .ReturnsAsync(OperationResult.Success());

            _itemsRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(OperationResult<ItemDto>.Failed("Not found").SetCode(404));

            // Act
            var result = await _sut.DeleteAsync(id);
            var searchAfter = await _sut.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            searchAfter.IsSuccessfulWithNoErrors.Should().BeFalse();
            searchAfter.Code.Should().Be(404);


        }
    }
}