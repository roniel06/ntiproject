using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTI.Application.InputModels.Core;

namespace NTI.Application.InputModels.Employee
{
    public class EmployeeInputModel : IIdAble
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}