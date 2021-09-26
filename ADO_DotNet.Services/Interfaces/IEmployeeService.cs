using ADO_DotNet.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DotNet.Services.Interfaces
{
    public interface IEmployeeService : IService<Employee>
    {
        public IEnumerable<Department> GetDepartments();
    }
}