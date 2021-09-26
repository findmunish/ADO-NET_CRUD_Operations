using ADO_DotNet.DAL.dbConfig;
using ADO_DotNet.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DotNet.DAL.Implementations
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private string _tableName;
        private string _primaryKeyName;
        private string _modelName;
        //private static readonly string _connectionString = "";   // TODO
        protected DatabaseContext _context;

        protected enum _CRUD_OPERATION_TYPE
        {
            GET_ALL = 1, GET_BY_ID, CREATE, UPDATE, DELETE
        }

        public Repository(DatabaseContext context, string tableName, string primaryKeyName, string modelName)
        {
            _context = context;
            _tableName = tableName;
            _primaryKeyName = primaryKeyName;
            _modelName = modelName;
        }

        private string _getProcedureName(_CRUD_OPERATION_TYPE opType)
        {
            string sqlProcedureName = "";

            switch (opType)
            {
                case _CRUD_OPERATION_TYPE.GET_ALL:
                    sqlProcedureName = $"usp_get{_modelName}s";
                    break;

                case _CRUD_OPERATION_TYPE.GET_BY_ID:
                    sqlProcedureName = $"usp_get{_modelName}ById";
                    break;

                case _CRUD_OPERATION_TYPE.CREATE:
                    sqlProcedureName = $"usp_add{_modelName}";
                    break;

                case _CRUD_OPERATION_TYPE.UPDATE:
                    sqlProcedureName = $"usp_update{_modelName}";
                    break;

                case _CRUD_OPERATION_TYPE.DELETE:
                    sqlProcedureName = $"usp_delete{_modelName}";
                    break;

                default:
                    sqlProcedureName = "";
                    break;
            }

            return sqlProcedureName;
        }

        public IEnumerable<TEntity> GetAll()
        {
            List<TEntity> listEntities = new List<TEntity>();

            using (SqlConnection sqlConn = new SqlConnection(_context.ConnectionString))
            {
                string sqlQuery = $"SELECT * FROM {_tableName}";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                //SqlCommand cmd = sqlConn.CreateCommand();
                //cmd.CommandText = _getProcedureName(_CRUD_OPERATION_TYPE.GET_ALL); 
                //cmd.CommandType = CommandType.StoredProcedure;

                sqlConn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    TEntity entity = copyEntity(dataReader);
                    listEntities.Add(entity);
                }

                sqlConn.Close();
            }

            return listEntities;
        }

        public TEntity GetById(int? id)
        {
            TEntity entity = null;

            using (SqlConnection sqlConn = new SqlConnection(_context.ConnectionString))
            {
                string sqlQuery = $"SELECT * FROM {_tableName} WHERE {_primaryKeyName} = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                //SqlCommand cmd = new SqlCommand(_getProcedureName(_CRUD_OPERATION_TYPE.GET_BY_ID), sqlConn);
                //cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter(_primaryKeyName, id);
                cmd.Parameters.Add(parameter);

                sqlConn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    entity = copyEntity(dataReader);
                }

                sqlConn.Close();
            }
            return entity;
        }

        public TEntity Create(TEntity newModel)
        {
            using (SqlConnection sqlConn = new SqlConnection(_context.ConnectionString))
            {
                string columns = getCreateParams();

                string sqlQuery = $"Insert into {_tableName} {columns}";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                //SqlCommand cmd = new SqlCommand(_getProcedureName(_CRUD_OPERATION_TYPE.CREATE), sqlConn);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd = addEntity(cmd, newModel);

                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                return newModel;
            }
        }

        public TEntity Update(int id, TEntity updatedModel)
        {
            using (SqlConnection sqlConn = new SqlConnection(_context.ConnectionString))
            {
                string columns = getUpdateParams();

                string sqlQuery = $"Update {_tableName} {columns} WHERE {_primaryKeyName} = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                //SqlCommand cmd = new SqlCommand(_getProcedureName(_CRUD_OPERATION_TYPE.UPDATE), sqlConn);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd = addEntity(cmd, updatedModel);

                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                return updatedModel;
            }
        }

        public void Delete(int? id)
        {
            using (SqlConnection sqlConn = new SqlConnection(_context.ConnectionString))
            {
                string sqlQuery = $"Delete FROM {_tableName} WHERE {_primaryKeyName} = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                //SqlCommand cmd = new SqlCommand(_getProcedureName(_CRUD_OPERATION_TYPE.DELETE), sqlConn);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue($"{_primaryKeyName}", id);

                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
        }

        protected abstract string getCreateParams();
        protected abstract string getUpdateParams();
        protected abstract TEntity copyEntity(SqlDataReader dataReader);
        protected abstract SqlCommand addEntity(SqlCommand cmd, TEntity entity);
    }
}