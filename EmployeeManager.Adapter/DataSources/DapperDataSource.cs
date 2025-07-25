using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using EmployeeManager.Adapter.TypeHandlers;
using Npgsql;

namespace EmployeeManager.Adapter.DataSources
{
    public class DapperDataSource<TModel> where TModel : class
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public DapperDataSource(string connectionString, string tableName)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));

            // Configuração específica para Dapper e Npgsql para lidar com GUIDs
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
        }

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

        // Helper para abrir a conexão
        protected IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<TModel> Load(Guid id)
        {
            using (var connection = CreateConnection())
            {
                // Dapper.Contrib.Get<T> gera o SELECT * FROM [TableName] WHERE Id = @Id
                var dataModel = await connection.GetAsync<TModel>(id);
                return dataModel;
            }
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            var sql = $"SELECT * FROM {_tableName}";
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<TModel>(sql);
            }
        }
        
        public async Task<IEnumerable<TModel>> GetByField(string fieldName, string fieldValue)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE \"{fieldName}\" = @FieldValue";
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<TModel>(sql, new { FieldValue = fieldValue });
            }
        }

        public async Task<TModel> Save(TModel model)
        {
            using var connection = GetConnection();

            bool updated = await connection.UpdateAsync(model);
            if (!updated)
            {
                await connection.InsertAsync(model);
            }

            return model;
        }

        public async Task<bool> Delete(TModel model)
        {
            using var connection = GetConnection();            
            return await connection.DeleteAsync(model);
        }

    }
}