using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.Adapter.DataSources;
using EmployeeManager.Adapter.Models;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Requests;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.Adapter.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperDataSource<EmployeeDataModel> _dapperDataSource;

        private readonly IMapper _mapper;

        public EmployeeRepository(IConfiguration configuration)
        {
            // TODO criar o Repository para nao expor o DataSource aqui
            var connectionString = configuration.GetConnectionString("Default");

            var tableName = configuration["DataBase:EmployeeTableName"];

            _dapperDataSource = new DapperDataSource<EmployeeDataModel>(connectionString, tableName);
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeEntity> Load(Guid id)
        {
            var model = await _dapperDataSource.Load(id);

            if (model == null)
                return null;

            return _mapper.Map<EmployeeEntity>(model); 
        }

        public Task<IEnumerable<EmployeeEntity>> Query(EmployeeFilterRequest filter)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeEntity> Save(EmployeeEntity documentModel)
        {
            throw new NotImplementedException();
        }
    }
}