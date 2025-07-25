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
            var connectionString = configuration.GetConnectionString("Default");

            var tableName = configuration["DataBase:EmployeeTableName"];

            _dapperDataSource = new DapperDataSource<EmployeeDataModel>(connectionString, tableName);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _dapperDataSource.Delete(new EmployeeDataModel() { Id = id });
        }

        public async Task<EmployeeEntity?> Load(Guid id)
        {
            var model = await _dapperDataSource.Load(id);

            if (model == null)
                return null;

            return _mapper.Map<EmployeeEntity>(model);
        }


        public async Task<EmployeeEntity> LoadByDocumentNumber(string documentNumber)
        {
            var resultList = await _dapperDataSource.GetByField("DocumentNumber", documentNumber);
            var model = resultList.FirstOrDefault();

            return _mapper.Map<EmployeeEntity>(model);
        }

        public async Task<IEnumerable<EmployeeEntity>> Query(EmployeeFilterRequest filter)
        {
            var resulAll = await _dapperDataSource.GetAll();

            var resultQuery = resulAll.Where(x => MatchFilter(x, filter));

            return _mapper.Map<IEnumerable<EmployeeEntity>>(resultQuery); ;
        }

        public bool MatchFilter(EmployeeDataModel model, EmployeeFilterRequest filter)
        {
            return
            (!filter.Active.HasValue || model.Active == filter.Active)
            &&
            (string.IsNullOrEmpty(filter.Email) || model.Email == filter.Email)
            &&
            (!filter.JobLevel.HasValue || model.JobLevel == filter.JobLevel)
            &&
            (string.IsNullOrEmpty(filter.PhoneNumber) || model.PhoneNumber == filter.PhoneNumber);            
        }

        public async Task<EmployeeEntity> Save(EmployeeEntity employeeEntity)
        {
            var model = _mapper.Map<EmployeeDataModel>(employeeEntity);
            var modelSaved = await _dapperDataSource.Save(model);

            return _mapper.Map<EmployeeEntity>(modelSaved);
        }
        
    }
}