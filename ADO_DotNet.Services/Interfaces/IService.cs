using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DotNet.Services.Interfaces
{
    public interface IService<TService> where TService : class
    {
        public IEnumerable<TService> GetAll();
        public TService GetById(int id);
        public void Create(TService deptModel);
        public void Update(int id, TService deptModel);
        public void Delete(int id);
        public TService _getModelById(int id, string renderView, string viewType);
    }
}