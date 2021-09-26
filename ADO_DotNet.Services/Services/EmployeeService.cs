using ADO_DotNet.DAL.Interfaces;
using ADO_DotNet.Models.Entities;
using ADO_DotNet.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ADO_DotNet.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IDepartmentRepository _deptRepo;

        public EmployeeService(IEmployeeRepository empRepo, IDepartmentRepository deptRepo)
        {
            _empRepo = empRepo;
            _deptRepo = deptRepo;
        }

        public IEnumerable<Employee> GetAll()
        {
            List<Employee> employees = _empRepo.GetAll().ToList();
            foreach (Employee emp in employees)
            {
                emp.Department = _deptRepo.GetById(emp.DeptId);
            }
            return employees;
        }

        public Employee GetById(int id)
        {
            return _empRepo.GetById(id);
        }

        public void Create(Employee empModel)
        {
            _empRepo.Create(empModel);
        }

        public void Update(int id, Employee empModel)
        {
            _empRepo.Update(id, empModel);
        }

        public void Delete(int id)
        {
            _empRepo.Delete(id);
        }

        public Employee _getModelById(int id, string renderView, string viewType)
        {
            Employee model = _empRepo.GetById(id);
            if (model == null)
            {
                return null;
            }

            model.Department = _deptRepo.GetById(model.DeptId);
            model.ViewType = viewType;
            model.RenderPage = renderView;

            return model;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _deptRepo.GetAll();
        }
    }
}
