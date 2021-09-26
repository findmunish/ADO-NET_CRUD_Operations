using ADO_DotNet.DAL.Interfaces;
using ADO_DotNet.Models.Entities;
using ADO_DotNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DotNet.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentService(IDepartmentRepository deptRepo)
        {
            _deptRepo = deptRepo;
        }

        public IEnumerable<Department> GetAll()
        {
            return _deptRepo.GetAll().ToList();
        }

        public Department GetById(int id)
        {
            return _deptRepo.GetById(id);
        }

        public void Create(Department deptModel)
        {
            _deptRepo.Create(deptModel);
        }

        public void Update(int id, Department deptModel)
        {
            _deptRepo.Update(id, deptModel);
        }

        public void Delete(int id)
        {
            _deptRepo.Delete(id);
        }

        public Department _getModelById(int id, string renderView, string viewType)
        {
            Department model = _deptRepo.GetById(id);
            if (model == null)
            {
                return null;
            }

            model.ViewType = viewType;
            model.RenderPage = renderView;

            return model;
        }
    }
}