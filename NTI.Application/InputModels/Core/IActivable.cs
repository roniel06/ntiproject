using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTI.Application.InputModels.Core
{
    public interface IActivable
    {
        public bool IsActive { get; set; }
    }
}