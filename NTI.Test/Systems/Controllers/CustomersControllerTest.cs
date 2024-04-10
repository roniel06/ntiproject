using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NTI.Api.Controllers;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Services;
using NTI.Application.OperationResultDtos;
using NTI.Application.Services;
using NTI.Domain.Models;

namespace NTI.Test.Systems.Controllers
{

    public class CustomersControllerTest
    {
        private readonly CustomersController _sut;
        private readonly Mock<ICustomersService> _customerService = new Mock<ICustomersService>();

        public CustomersControllerTest()
        {
            _sut = new CustomersController(_customerService.Object);
        }


        [Fact]
        public async Task GetCustomers_ReturnsOk()
        {
            // Arrange
            var fixture = new Fixture();
            var customersList = fixture.Build<CustomerDto>()
                .Without(x => x.CustomerItems).CreateMany(5);
            _customerService.Setup(x => x.GetAllAsync()).ReturnsAsync(OperationResult<IEnumerable<CustomerDto>>.Success(customersList));

            // Act
            var result = (OkObjectResult)await _sut.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetCustomers_ReturnsOkWithNoRecords()
        {
            // Arrange
            _customerService.Setup(x => x.GetAllAsync()).ReturnsAsync(OperationResult<IEnumerable<CustomerDto>>.Success(new List<CustomerDto>()));

            // Act
            var result = (OkObjectResult)await _sut.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var dto = result.Value.Should().BeAssignableTo<IEnumerable<CustomerDto>>().Subject;
            dto.Count().Should().Be(0);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsOk()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Create<CustomerInputModel>();
            var dto = fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create();
            _customerService.Setup(x => x.CreateAsync(It.IsAny<CustomerInputModel>())).ReturnsAsync(OperationResult<CustomerDto>.Success(dto));
            // Act
            var result = (OkObjectResult)await _sut.Create(customer);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task UpdateCustomer_ShouldReturnOk()
        {
            // Arrange
            var fixture = new Fixture();
            var item = fixture.Build<CustomerInputModel>().Create();
            var ret = OperationResult<CustomerDto>.Success(fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create());
            _customerService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<CustomerInputModel>())).ReturnsAsync(ret);

            // Act
            var result = (OkObjectResult)await _sut.Update(1, item);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var dto = result.Value.Should().BeAssignableTo<CustomerDto>().Subject;
            dto.Name.Should().Be(ret.Payload.Name);
            dto.LastName.Should().Be(ret.Payload.LastName);
            dto.Phone.Should().Be(ret.Payload.Phone);
            dto.Email.Should().Be(ret.Payload.Email);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCreatedRecord()
        {
            // Arrange 
            var fixture = new Fixture();
            var customer = fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create();

            _customerService.Setup(x => x.GetByIdAsync(customer.Id))
                .ReturnsAsync(OperationResult<CustomerDto>.Success(customer));

            // Act
            var result = (OkObjectResult)await _sut.GetById(customer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<CustomerDto>().Subject;
            dto.Id.Should().Be(customer.Id);
            dto.Name.Should().Be(customer.Name);
            dto.LastName.Should().Be(customer.LastName);
            dto.Phone.Should().Be(customer.Phone);
            dto.Email.Should().Be(customer.Email);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOk()
        {
            // Arrange 
            var fixture = new Fixture();
            var customer = fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create();

            _customerService.Setup(x => x.DeleteAsync(customer.Id))
                .ReturnsAsync(OperationResult.Success());

            // Act
            var result = (OkObjectResult)await _sut.Delete(customer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var dto = result.Value.Should().BeAssignableTo<OperationResult>().Subject;
            dto.IsSuccessfulWithNoErrors.Should().BeTrue();
        }


    }
}