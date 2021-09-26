using ADO_DotNet.Models.Entities;
using ADO_DotNet.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ADO_DotNet.DAL.dbConfig;

namespace ADO_DotNet.DAL.Implementations
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private static readonly string _tableName = "Employees";
        private static readonly string _pKey = "EmpId";
        private static readonly string _modelString = "Employee";

        public EmployeeRepository(DatabaseContext context) : base(context, _tableName, _pKey, _modelString) { }

        protected override Employee copyEntity(SqlDataReader dataReader)
        {
            Employee empEntity = new Employee();

            if (!dataReader.HasRows)
            {
                return empEntity;
            }

            empEntity.EmpId = Convert.ToInt32(dataReader["EmpId"]);
            empEntity.Name = dataReader["Name"].ToString();
            empEntity.Address = dataReader["Address"].ToString();
            empEntity.ImagePath = dataReader["ImagePath"].ToString();
            empEntity.DeptId = Convert.ToInt32(dataReader["DeptId"]);

            return empEntity;
        }

        protected override SqlCommand addEntity(SqlCommand cmd, Employee empEntity)
        {
            cmd.Parameters.AddWithValue("@Name", empEntity.Name);
            cmd.Parameters.AddWithValue("@Address", empEntity.Address);
            cmd.Parameters.AddWithValue("@ImagePath", empEntity.ImagePath);
            cmd.Parameters.AddWithValue("@DeptId", empEntity.DeptId);

            return cmd;
        }

        protected override string getCreateParams()
        {
            return $"(Name, Address, ImagePath, DeptId) VALUES (@Name, @Address, @ImagePath, @DeptId)";
        }

        protected override string getUpdateParams()
        {
            return $"SET Name = @Name, Address = @Address, ImagePath = @ImagePath, DeptId = @DeptId ";
        }
    }
}