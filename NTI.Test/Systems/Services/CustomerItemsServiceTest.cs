

using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Mappings.Customers;
using NTI.Application.Mappings.CustomersItems;
using NTI.Application.Mappings.Items;
using NTI.Application.OperationResultDtos;
using NTI.Application.Services;

namespace NTI.Test.Systems.Services
{
    public class CustomerItemsServiceTest
    {
        private readonly ICustomerItemService _sut;
        private Mock<ICustomerItemsRepository> _customerItemsRepositoryMock = new Mock<ICustomerItemsRepository>();
        private IMapper _mapper;

        public CustomerItemsServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
       {
           cfg.AddProfile<CustomerMapping>();
           cfg.AddProfile<CustomerItemsMap>();
           cfg.AddProfile<ItemMapping>();

       });

            _mapper = config.CreateMapper();
            _sut = new CustomerItemsService(_customerItemsRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnOperationResultWithCustomerItemsDto_WhenCustomerItemInputModelIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemInputModel = fixture.Create<CustomerItemInputModel>();
            var customerItemsDto = _mapper.Map<CustomerItemsDto>(customerItemInputModel);

            _customerItemsRepositoryMock.Setup(x => x.CreateAsync(customerItemInputModel, null))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItemsDto));

            // Act
            var result = await _sut.CreateAsync(customerItemInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().Be(customerItemsDto);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnOperationResultWithErrors_WhenCustomerItemInputModelIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemInputModel = fixture.Create<CustomerItemInputModel>();
            var customerItemsDto = _mapper.Map<CustomerItemsDto>(customerItemInputModel);

            _customerItemsRepositoryMock.Setup(x => x.CreateAsync(customerItemInputModel, null))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Failed().AddError("The input value should not be null"));

            // Act
            var result = await _sut.CreateAsync(customerItemInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOperationResultWithAllCustomerItems()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
            .Without(x => x.Customer)
            .Without(x => x.Item)
            .Create();

            _customerItemsRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(OperationResult<IEnumerable<CustomerItemsDto>>.Success(new List<CustomerItemsDto> { customerItemsDto }));

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<IEnumerable<CustomerItemsDto>>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOperationResultWithCustomerItemsDto_WhenIdIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
           .Without(x => x.Customer)
           .Without(x => x.Item)
           .Create();

            _customerItemsRepositoryMock.Setup(x => x.GetByIdAsync(customerItemsDto.Id))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItemsDto));

            // Act
            var result = await _sut.GetByIdAsync(customerItemsDto.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().Be(customerItemsDto);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOperationResultWithErrors_WhenIdIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
           .Without(x => x.Customer)
           .Without(x => x.Item)
           .Create();
            _customerItemsRepositoryMock.Setup(x => x.GetByIdAsync(customerItemsDto.Id))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Failed().AddError("Customer item not found"));

            // Act
            var result = await _sut.GetByIdAsync(customerItemsDto.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnOperationResultWithCustomerItemsDto_WhenIdAndCustomerItemInputModelAreValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
          .Without(x => x.Customer)
          .Without(x => x.Item)
          .Create();
            var customerItemInputModel = fixture.Create<CustomerItemInputModel>();

            _customerItemsRepositoryMock.Setup(x => x.EditAsync(customerItemsDto.Id, customerItemInputModel, null))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItemsDto));

            // Act
            var result = await _sut.UpdateAsync(customerItemsDto.Id, customerItemInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().Be(customerItemsDto);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOperationResultWithCustomerItemsDto_WhenIdIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
            .Without(x => x.Customer)
            .Without(x => x.Item)
            .Create();

            _customerItemsRepositoryMock.Setup(x => x.SoftDeleteByIdAsync(customerItemsDto.Id))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Success(customerItemsDto));

            // Act
            var result = await _sut.DeleteAsync(customerItemsDto.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOperationResultWithErrors_WhenIdIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
            .Without(x => x.Customer)
            .Without(x => x.Item)
            .Create();

            _customerItemsRepositoryMock.Setup(x => x.SoftDeleteByIdAsync(customerItemsDto.Id))
                .ReturnsAsync(OperationResult<CustomerItemsDto>.Failed().AddError("Customer item not found"));

            // Act
            var result = await _sut.DeleteAsync(customerItemsDto.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerItemsDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetByCustomerIdAsync_ShouldReturnOperationResultWithAllCustomerItems_WhenCustomerIdIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customerItemsDto = fixture.Build<CustomerItemsDto>()
            .Without(x => x.Customer)
            .Without(x => x.Item)
            .Create();

            _customerItemsRepositoryMock.Setup(x => x.GetByCustomerIdAsync(customerItemsDto.CustomerId))
                .ReturnsAsync(OperationResult<IEnumerable<CustomerItemsDto>>.Success(new List<CustomerItemsDto> { customerItemsDto }));

            // Act
            var result = await _sut.GetByCustomerIdAsync(customerItemsDto.CustomerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<IEnumerable<CustomerItemsDto>>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeEmpty();
        }

    }
}