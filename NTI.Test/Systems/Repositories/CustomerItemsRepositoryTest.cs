
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Mappings.Customers;
using NTI.Application.Mappings.CustomersItems;
using NTI.Application.Mappings.Items;
using NTI.Domain.Models;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories;
using NTI.Test.Helpers;

namespace NTI.Test.Systems.Repositories
{
    public class CustomerItemsRepositoryTest
    {
        private readonly CustomerItemsRepository _sut;
        private readonly ProjectDbContext _dbContext;
        private readonly IMapper _mapper;
        private int customerId = 0;
        private int itemId = 0;

        public CustomerItemsRepositoryTest()
        {
            _dbContext = TestDbContextFactory.Create();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerMapping>();
                cfg.AddProfile<CustomerItemsMap>();
                cfg.AddProfile<ItemMapping>();

            });

            _mapper = config.CreateMapper();
            _sut = new CustomerItemsRepository(_dbContext, _mapper);
            Setup();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCustomerItem_WhenCustomerItemIsValid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            // Act
            var result = await _sut.CreateAsync(customerInput);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Id.Should().BeGreaterThan(0);
            result.Payload.CustomerId.Should().Be(customerInput.CustomerId);
            result.Payload.ItemId.Should().Be(customerInput.ItemId);
            result.Payload.Quantity.Should().Be(customerInput.Quantity);
            result.Payload.Price.Should().Be(customerInput.Price);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenCustomerItemIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, 0)
                                       .With(c => c.ItemId, 0)
                                       .Create();

            // Act
            var result = await _sut.CreateAsync(customerInput);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnCustomerItem_WhenCustomerItemIsValid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            var customerItem = await _sut.CreateAsync(customerInput);

            customerItem.Payload.Quantity = 10;
            customerItem.Payload.Price = 1000;
            var mappedToInput = _mapper.Map<CustomerItemInputModel>(customerItem.Payload);

            // Act
            var result = await _sut.EditAsync(mappedToInput.Id ?? 0, mappedToInput);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Id.Should().BeGreaterThan(0);
            result.Payload.CustomerId.Should().Be(customerInput.CustomerId);
            result.Payload.ItemId.Should().Be(customerInput.ItemId);
            result.Payload.Quantity.Should().Be(customerItem.Payload.Quantity);
            result.Payload.Price.Should().Be(customerItem.Payload.Price);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenCustomerItemIsInvalid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            var customerItem = await _sut.CreateAsync(customerInput);

            customerItem.Payload.Quantity = 10;
            customerItem.Payload.Price = 1000;
            var mappedToInput = _mapper.Map<CustomerItemInputModel>(customerItem.Payload);
            mappedToInput.CustomerId = 0;
            mappedToInput.ItemId = 0;

            // Act
            var result = await _sut.EditAsync(mappedToInput.Id ?? 0, mappedToInput);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnCustomerItem_WhenCustomerItemIsValid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            var customerItem = await _sut.CreateAsync(customerInput);

            // Act
            var result = await _sut.SoftDeleteByIdAsync(customerItem.Payload.Id);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomerItem_WhenCustomerItemIsValid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            var customerItem = await _sut.CreateAsync(customerInput);

            // Act
            var result = await _sut.GetByIdAsync(customerItem.Payload.Id);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Id.Should().Be(customerItem.Payload.Id);
            result.Payload.CustomerId.Should().Be(customerItem.Payload.CustomerId);
            result.Payload.ItemId.Should().Be(customerItem.Payload.ItemId);
            result.Payload.Quantity.Should().Be(customerItem.Payload.Quantity);
            result.Payload.Price.Should().Be(customerItem.Payload.Price);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCustomerItems_WhenCustomerItemsExist()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            await _sut.CreateAsync(customerInput);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeEmpty();
            result.Payload.Should().HaveCount(1);
        }


        private async void Setup()
        {
            var fixture = new Fixture();
            var customer = fixture.Build<Customer>()
                             .Without(x => x.Id)
                             .Without(x => x.CustomerItems)
                             .Create();

            var item = fixture.Build<Item>()
                              .Without(x => x.Id)
                              .Without(x => x.CustomerItems)
                              .With(i => i.DefaultPrice, 100)
                              .Create();

            _dbContext.Customers.Add(customer);
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
            itemId = item.Id;
            customerId = customer.Id;
        }

        [Fact]
        public async Task GetByCustomerIdAsync_ShouldReturnCustomerItems_WhenCustomerIdIsValid()
        {
            // Arrange
            var fixture = new Fixture();

            var customerInput = fixture.Build<CustomerItemInputModel>()
                                       .With(c => c.CustomerId, customerId)
                                       .With(c => c.ItemId, itemId)
                                       .Create();

            await _sut.CreateAsync(customerInput);

            // Act
            var result = await _sut.GetByCustomerIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeEmpty();
            result.Payload.Should().HaveCount(1);
        }
    }


}
