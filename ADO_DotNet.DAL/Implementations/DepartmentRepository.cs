using ADO_DotNet.Models.Entities;
using ADO_DotNet.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO_DotNet.DAL.dbConfig;

namespace ADO_DotNet.DAL.Implementations
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private static readonly string _tableName = "Departments";
        private static readonly string _pKey = "DeptId";
        private static readonly string _modelString = "Department";

        public DepartmentRepository(DatabaseContext context) : base(context, _tableName, _pKey, _modelString) { }

        protected override Department copyEntity(SqlDataReader dataReader)
        {
            Department deptEntity = new Department();

            if (!dataReader.HasRows)
            {
                return deptEntity;
            }

            deptEntity.DeptId = Convert.ToInt32(dataReader["DeptId"]);
            deptEntity.Name = dataReader["Name"].ToString();

            return deptEntity;
        }

        protected override SqlCommand addEntity(SqlCommand cmd, Department deptEntity)
        {
            cmd.Parameters.AddWithValue("@Name", deptEntity.Name);

            return cmd;
        }

        protected override string getCreateParams()
        {
            return $"(Name) VALUES (@Name) ";
        }

        protected override string getUpdateParams()
        {
            return $"SET Name = @Name ";
        }
    }
}
