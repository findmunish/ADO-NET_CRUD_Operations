using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADO_DotNet.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> GetAll();
        TEntity GetById(int? id);
        public TEntity Create(TEntity newModel);
        public TEntity Update(int id, TEntity updateModel);
        public void Delete(int? id);
    }
}