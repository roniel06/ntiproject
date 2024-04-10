using AutoFixture;
using Moq;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Services;
using FluentAssertions;
using NTI.Application.Dtos;
using NTI.Application.OperationResultDtos;
using AutoMapper;
using NTI.Application.Mappings.Customers;
using NTI.Application.Mappings.CustomersItems;
using NTI.Application.Mappings.Items;

namespace NTI.Test.Systems.Services
{
    public class CustomersServiceTest
    {

        private readonly Mock<ICustomersRepository> _customersRepositoryMock = new Mock<ICustomersRepository>();
        private readonly ICustomersService _sut;
        private readonly IMapper _mapper;

        public CustomersServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<CustomerMapping>();
               cfg.AddProfile<CustomerItemsMap>();
               cfg.AddProfile<ItemMapping>();

           });

            _mapper = config.CreateMapper();
            _sut = new CustomersService(_customersRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnOperationResultWithCustomerDto_WhenCustomerInputModelIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerInputModel = fixture.Create<CustomerInputModel>();
            var customerDto = _mapper.Map<CustomerDto>(customerInputModel);

            _customersRepositoryMock.Setup(x => x.CreateAsync(customerInputModel, null))
                .ReturnsAsync(OperationResult<CustomerDto>.Success(customerDto));

            // Act
            var result = await _sut.CreateAsync(customerInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().Be(customerDto);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnOperationResultWithErrors_WhenCustomerInputModelIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerInputModel = fixture.Build<CustomerInputModel>().Without(x => x.Name).Create();
            _customersRepositoryMock.Setup(x => x.CreateAsync(customerInputModel, null))
                            .ReturnsAsync(OperationResult<CustomerDto>.Failed("Name is required"));

            // Act
            var result = await _sut.CreateAsync(customerInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOperationResultWithAllCustomers()
        {
            // Arrange
            var fixture = new Fixture();
            var customers = fixture.Build<CustomerDto>()
                .Without(x => x.CustomerItems)
                .CreateMany(3);
            _customersRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(OperationResult<IEnumerable<CustomerDto>>.Success(customers));

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<IEnumerable<CustomerDto>>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Count().Should().Be(3);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOperationResultWithCustomerDto()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create();

            _customersRepositoryMock.Setup(x => x.GetByIdAsync(customer.Id))
                .ReturnsAsync(OperationResult<CustomerDto>.Success(customer));

            // Act
            var result = await _sut.GetByIdAsync(customer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().Be(customer);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOperationResultWithErrors_WhenCustomerNotFound()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create();

            _customersRepositoryMock.Setup(x => x.GetByIdAsync(customer.Id))
                .ReturnsAsync(OperationResult<CustomerDto>.Failed("Customer not found"));

            // Act
            var result = await _sut.GetByIdAsync(customer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOperationResultWithCustomerDto_WhenCustomerInputModelIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerInputModel = fixture.Create<CustomerInputModel>();
            var customerDto = _mapper.Map<CustomerDto>(customerInputModel);

            _customersRepositoryMock.Setup(x => x.EditAsync(customerDto.Id, customerInputModel, null))
                .ReturnsAsync(OperationResult<CustomerDto>.Success(customerDto));

            // Act
            var result = await _sut.UpdateAsync(customerDto.Id, customerInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().Be(customerDto);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOperationResultWithErrors_WhenCustomerInputModelIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerInputModel = fixture.Build<CustomerInputModel>().Without(x => x.Name).Create();
            var customerDto = _mapper.Map<CustomerDto>(customerInputModel);

            _customersRepositoryMock.Setup(x => x.EditAsync(customerDto.Id, customerInputModel, null))
                .ReturnsAsync(OperationResult<CustomerDto>.Failed("Name is required"));

            // Act
            var result = await _sut.UpdateAsync(customerDto.Id, customerInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessOperationResult()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Build<CustomerDto>().Without(x => x.CustomerItems).Create();

            _customersRepositoryMock.Setup(x => x.SoftDeleteByIdAsync(customer.Id))
                .ReturnsAsync(OperationResult.Success());

            _customersRepositoryMock.Setup(x => x.GetByIdAsync(customer.Id))
                .ReturnsAsync(OperationResult<CustomerDto>.Failed("Not found").SetCode(404));

            // Act
            var result = await _sut.DeleteAsync(customer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Code.Should().Be(200);
        }
    }
}