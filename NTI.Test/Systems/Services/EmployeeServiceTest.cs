using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Mappings.Employees;
using NTI.Application.OperationResultDtos;
using NTI.Application.Services;
using NTI.Application.Utils;
using NTI.Domain.Models;

namespace NTI.Test.Systems.Services
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        private IMapper _mapper;
        private IEmployeeService _sut;

        public EmployeeServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
          {
              cfg.AddProfile<EmployeeMapping>();

          });

            _mapper = config.CreateMapper();
            _sut = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnOperationResultWithEmployeeDto_WhenEmployeeInputModelIsValid()
        {
            // Arrange
            var fixture = new Fixture();
            var employeeInputModel = fixture.Create<EmployeeInputModel>();
            var employeeDto = _mapper.Map<EmployeeDto>(employeeInputModel);

            _employeeRepositoryMock.Setup(x => x.CreateAsync(employeeInputModel, null))
                .ReturnsAsync(OperationResult<EmployeeDto>.Success(employeeDto));

            // Act
            var result = await _sut.CreateAsync(employeeInputModel);

            // Assert
            result.IsSuccessfulWithNoErrors.Should().BeTrue();
            result.Payload.Should().NotBeNull();
            result.Should().BeOfType<OperationResult<EmployeeDto>>();
            result.Payload.Should().Be(employeeDto);
        }

    }
}