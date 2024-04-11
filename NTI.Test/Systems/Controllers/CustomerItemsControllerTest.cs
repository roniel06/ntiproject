using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NTI.Api.Controllers;
using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Services;
using NTI.Application.OperationResultDtos;

namespace NTI.Test.Systems.Controllers
{
    public class CustomerItemsControllerTest
    {
        private readonly CustomerItemsController _sut;
        private readonly Mock<ICustomerItemService> _mockCustomerItemService = new Mock<ICustomerItemService>();

        public CustomerItemsControllerTest()
        {
            _sut = new CustomerItemsController(_mockCustomerItemService.Object);
        }


        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenCustomerItemsExist()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItems = fixture.Build<CustomerItemsDto>()
            .Without(x=> x.Customer)
            .Without(x=> x.Item).CreateMany(3);

            _mockCustomerItemService.Setup(x => x.GetAllAsync())
                .ReturnsAsync(OperationResult<IEnumerable<CustomerItemsDto>>.Success(customerItems));

            // Act
            var result = (OkObjectResult)await _sut.Getall();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult<IEnumerable<CustomerItemsDto>>>().Subject;
            dto.Payload.Should().HaveCount(3);
            dto.Payload.Should().BeEquivalentTo(customerItems);
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenCustomerItemExist()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItem = fixture.Build<CustomerItemsDto>()
            .Without(x=> x.Customer)
            .Without(x=> x.Item).Create();

            _mockCustomerItemService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItem));

            // Act
            var result = (OkObjectResult)await _sut.Get(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult<CustomerItemsDto>>().Subject;
            dto.Payload.Should().BeEquivalentTo(customerItem);
        }

        [Fact]
        public async Task GetByCustomerId_ShouldReturnOk_WhenCustomerItemsExist()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItems = fixture.Build<CustomerItemsDto>()
            .Without(x=> x.Customer)
            .Without(x=> x.Item).CreateMany(3);

            _mockCustomerItemService.Setup(x => x.GetByCustomerIdAsync(It.IsAny<int>()))
                .ReturnsAsync(OperationResult<IEnumerable<CustomerItemsDto>>.Success(customerItems));

            // Act
            var result = (OkObjectResult)await _sut.GetByCustomerId(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult<IEnumerable<CustomerItemsDto>>>().Subject;
            dto.Payload.Should().HaveCount(3);
            dto.Payload.Should().BeEquivalentTo(customerItems);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenCustomerItemUpdated()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItem = fixture.Build<CustomerItemsDto>()
            .Without(x=> x.Customer)
            .Without(x=> x.Item).Create();

            _mockCustomerItemService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<CustomerItemInputModel>()))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItem));

            // Act
            var result = (OkObjectResult)await _sut.Update(1, new CustomerItemInputModel());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult<CustomerItemsDto>>().Subject;
            dto.Payload.Should().BeEquivalentTo(customerItem);
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenCustomerItemCreated()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItem = fixture.Build<CustomerItemsDto>()
            .Without(x=> x.Customer)
            .Without(x=> x.Item).Create();

            _mockCustomerItemService.Setup(x => x.CreateAsync(It.IsAny<CustomerItemInputModel>()))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItem));

            // Act
            var result = (OkObjectResult)await _sut.Post(new CustomerItemInputModel());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult<CustomerItemsDto>>().Subject;
            dto.Payload.Should().BeEquivalentTo(customerItem);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenCustomerItemDeleted()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItem = fixture.Build<CustomerItemsDto>()
            .Without(x=> x.Customer)
            .Without(x=> x.Item).Create();

            _mockCustomerItemService.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItem));

            // Act
            var result = (OkObjectResult)await _sut.Delete(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult<CustomerItemsDto>>().Subject;
            dto.Payload.Should().BeEquivalentTo(customerItem);
        }
    }
}