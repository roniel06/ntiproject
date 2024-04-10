using AutoMapper;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Mappings.Customers;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories;
using NTI.Test.Helpers;
using AutoFixture;
using FluentAssertions;
using NTI.Application.Dtos;
using NTI.Application.OperationResultDtos;
using NTI.Domain.Models;
using NTI.Application.Mappings.CustomersItems;
using NTI.Application.Mappings.Items;

namespace NTI.Test.Systems.Repositories
{
    public class CustomersRepositoryTest
    {
        private readonly CustomersRepository _sut;
        private readonly ProjectDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomersRepositoryTest()
        {
            _dbContext = TestDbContextFactory.Create();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMapping>();
                cfg.AddProfile<CustomerItemsMap>();
                cfg.AddProfile<ItemMapping>();
                
            });

            _mapper = config.CreateMapper();
            _sut = new CustomersRepository(_dbContext, _mapper);
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnCustomer_WhenCustomerIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Create<CustomerInputModel>();

            // Act
            var result = await _sut.CreateAsync(customer);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Name.Should().Be(customer.Name);
            result.Payload.Email.Should().Be(customer.Email);
            result.Payload.LastName.Should().Be(customer.LastName);
            result.Payload.Phone.Should().Be(customer.Phone);
            result.Payload.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task EditAsync_ShouldCreateAndEditCreatedRecord()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<CustomerInputModel>()
                .With(x => x.Name, "Initial Name")
                .With(x => x.LastName, "Initial LastName")
                .With(x => x.IsActive, true)
                .With(x => x.Email, "InitialEmail@email.com")
                .With(x => x.Phone, "1234567890")
            .Create();

            // Act
            await _sut.AddAsync(item);
            var result = await _sut.CommitAsync();


            item.Name = "Edited Name";
            item.LastName = "Edited LastName";
            item.IsActive = false;
            item.Email = "mynewEmail@test.com";
            item.Phone = "0987654321";
            var editedResult = await _sut.EditAsync(item.Id ?? 0, item);

            // Assert
            editedResult.Should().NotBeNull();
            editedResult.Should().BeOfType<OperationResult<CustomerDto>>();
            editedResult.IsSuccessfulWithNoErrors.Should().BeTrue();

            editedResult.Payload.Name.Should().Be(item.Name);
            editedResult.Payload.LastName.Should().Be(item.LastName);
            editedResult.Payload.Email.Should().Be(item.Email);
            editedResult.Payload.Phone.Should().Be(item.Phone);
            editedResult.Payload.IsActive.Should().Be(false);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCreateAndDeleteRecord()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<CustomerInputModel>()
                .With(x => x.Name, "Initial Name")
                .With(x => x.LastName, "Initial LastName")
                .With(x => x.IsActive, true)
                .With(x => x.Email, "test@test.com").Create();

            // Act
            await _sut.AddAsync(item);
            var result = await _sut.CommitAsync();
            var deletedResult = await _sut.SoftDeleteByIdAsync(item.Id ?? 0);

            // Assert
            deletedResult.Should().NotBeNull();
            deletedResult.Should().BeOfType<OperationResult?>();
            deletedResult.IsSuccessfulWithNoErrors.Should().BeTrue();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var fixture = new Fixture();
            var item = fixture.Build<CustomerInputModel>()
                .Create();

            // Act
            await _sut.AddAsync(item);
            await _sut.CommitAsync();
            var result = await _sut.GetByIdAsync(item.Id ?? 0);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<CustomerDto>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Id.Should().Be(item.Id);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllCreatedRecords(){
            // Arrange
            var fixture = new Fixture();
            var item = fixture.Build<CustomerInputModel>()
                .CreateMany(10);

            // Act
            foreach (var customer in item)
            {
                await _sut.AddAsync(customer);
            }
            await _sut.CommitAsync();
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<IEnumerable<CustomerDto>>>();
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeEmpty();
        }
    }
}
