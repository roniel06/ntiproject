using AutoMapper;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Mappings.Employees;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories;
using NTI.Test.Helpers;
using AutoFixture;
using NTI.Domain.Models;
using FluentAssertions;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;


namespace NTI.Test.Systems.Repositories
{
    public class EmployeeRepositoryTest
    {
        private readonly EmployeeRepository _sut;
        private readonly ProjectDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepositoryTest()
        {
            var config = new MapperConfiguration(cfg =>
          {
              cfg.AddProfile<EmployeeMapping>();
          });

            _mapper = config.CreateMapper();
            _dbContext = TestDbContextFactory.Create();
            _sut = new EmployeeRepository(_dbContext, _mapper);
        }


        [Fact]
        public async Task GetAllEmployees_ShouldReturn_AllCreatedEmployees()
        {
            //Arrange
            var fixture = new Fixture();
            var employees = fixture.Build<EmployeeInputModel>()
            .Without(x => x.Id)
            .CreateMany(5);

            foreach (var employee in employees)
            {
                await _sut.CreateAsync(employee);
            }

            //Act
            var result = await _sut.GetAllAsync();

            //Assert
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturn_EmployeeWithGivenId()
        {
            //Arrange
            var fixture = new Fixture();
            var employee = fixture.Create<EmployeeInputModel>();
            var created =await _sut.CreateAsync(employee);

            //Act
            var result = await _sut.GetByIdAsync(created.Payload.Id);

            //Assert
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Id.Should().Be(created.Payload.Id);
            result.Payload.FirstName.Should().Be(employee.FirstName);
            result.Payload.LastName.Should().Be(employee.LastName);
            result.Payload.Email.Should().Be(employee.Email);
        }

        [Fact]
        public async Task CreateEmployee_ShouldCreate_Employee()
        {
            //Arrange
            var fixture = new Fixture();
            var employee = fixture.Create<EmployeeInputModel>();

            //Act
            var result = await _sut.CreateAsync(employee);

            //Assert
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Payload.Id.Should().NotBe(0);
            result.Payload.FirstName.Should().Be(employee.FirstName);
            result.Payload.LastName.Should().Be(employee.LastName);
        }
    }
}