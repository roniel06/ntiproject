using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NTI.Api.Controllers;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Services;
using NTI.Application.OperationResultDtos;

namespace NTI.Test.Systems.Controllers
{
    public class ItemsControllerTest
    {

        private readonly ItemsController _sut;
        private readonly Mock<IItemsService> _itemsService = new Mock<IItemsService>();

        public ItemsControllerTest()
        {
            _sut = new ItemsController(_itemsService.Object);
        }
        [Fact]
        public async Task GetItems_ReturnsOk()
        {
            // Arrange
            var fixture = new Fixture();
            var itemsList = fixture.CreateMany<ItemDto>(5);
            _itemsService.Setup(x => x.GetAllAsync()).ReturnsAsync(OperationResult<IEnumerable<ItemDto>>.Success(itemsList));

            // Act
            var result = (OkObjectResult)await _sut.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetItems_ReturnsOkWithNoRecords()
        {
            // Arrange
            _itemsService.Setup(x => x.GetAllAsync()).ReturnsAsync(OperationResult<IEnumerable<ItemDto>>.Failed());

            // Act
            var result = (OkObjectResult)await _sut.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var dto = result.Value.Should().BeAssignableTo<OperationResult<IEnumerable<ItemDto>>>().Subject;
            dto.IsSuccessfulWithNoErrors.Should().BeFalse();
        }

        [Fact]
        public async Task CreateItem_ReturnsOk()
        {
            // Arrange
            var fixture = new Fixture();
            var item = fixture.Create<ItemInputModel>();
            _itemsService.Setup(x => x.CreateAsync(It.IsAny<ItemInputModel>())).ReturnsAsync(OperationResult<ItemDto>.Success(fixture.Create<ItemDto>()));

            // Act
            var result = (OkObjectResult)await _sut.Create(item);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task UpdateItem_ShouldReturnOk(){

            // Arrange
            var fixture = new Fixture();
            var item = fixture.Create<ItemInputModel>();
            var ret = OperationResult<ItemDto>.Success(fixture.Create<ItemDto>());
            _itemsService.Setup(x => x.UpdateAsync(It.IsAny<int>(),It.IsAny<ItemInputModel>())).ReturnsAsync(ret);

            // Act
            var result = (OkObjectResult)await _sut.Update(1, item);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var dto = result.Value.Should().BeAssignableTo<OperationResult<ItemDto>>().Subject;
            dto.IsSuccessfulWithNoErrors.Should().BeTrue();
            dto.Payload.Should().NotBeNull();
            dto.Payload.Description.Should().Be(ret.Payload.Description);
            dto.Payload.DefaultPrice.Should().Be(ret.Payload.DefaultPrice);
        }

        [Fact]
        public async Task GetItemByItemNumber_ShouldReturnOk(){

            // Arrange
            var fixture = new Fixture();
            var itemNumber = fixture.Create<int>();
            _itemsService.Setup(x => x.GetByItemNumberAsync(It.IsAny<int>())).ReturnsAsync(OperationResult<ItemDto>.Success(fixture.Create<ItemDto>()));

            // Act
            var result = (OkObjectResult)await _sut.GetByItemNumber(itemNumber);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task DeleteItem_ShouldReturnOk(){

            // Arrange
            var fixture = new Fixture();
            var itemNumber = fixture.Create<int>();
            _itemsService.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(OperationResult<ItemDto>.Success(fixture.Create<ItemDto>()));

            // Act
            var result = (OkObjectResult)await _sut.Delete(itemNumber);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}