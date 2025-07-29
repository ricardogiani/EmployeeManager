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

        public EmployeeRepository(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;

            var connectionString = configuration.GetConnectionString("Default");

            var tableName = configuration["DataBase:EmployeeTableName"];

            _dapperDataSource = new DapperDataSource<EmployeeDataModel>(connectionString, tableName);
        }

        public async Task<bool> Delete(int id)
        {
            return await _dapperDataSource.Delete(new EmployeeDataModel() { id = id });
        }

        public async Task<EmployeeEntity?> Load(int id)
        {
            var model = await _dapperDataSource.Load(id);

            if (model == null)
                return null;

            var entity = _mapper.Map<EmployeeEntity>(model);
            await LoadManager(entity);

            return entity;
        }

        public async Task<EmployeeEntity> LoadByDocumentNumber(string documentNumber)
        {
            var resultList = await _dapperDataSource.GetByField("document_number", documentNumber);
            var model = resultList.FirstOrDefault();

            var entity = _mapper.Map<EmployeeEntity>(model);
            await LoadManager(entity);

            return entity;
        }

        public async Task<IEnumerable<EmployeeEntity>> Query(EmployeeFilterRequest filter)
        {
            var resulAll = await _dapperDataSource.GetAll();

            var resultQuery = resulAll.Where(x => MatchFilter(x, filter)).ToList();

            //var result = _mapper.Map<EmployeeEntity>(new EmployeeDataModel() { BirthDate = DateTime.Now.AddYears(-20), FirstName = "asdasd", DocumentNumber = "asdadasdas", LastName = "asdadadasd", Email = "ricardogiani@gmail.com", JobLevel = Domain.Enums.JobLevelEnum.Coordinator });

            return _mapper.Map<IEnumerable<EmployeeEntity>>(resultQuery);
        }

        public bool MatchFilter(EmployeeDataModel model, EmployeeFilterRequest filter)
        {
            return
            (!filter.Active.HasValue || model.active == filter.Active)
            &&
            (string.IsNullOrEmpty(filter.Email) || model.email == filter.Email)
            &&
            (!filter.JobLevel.HasValue || ((filter.JobLevelUp == true && model.job_level > filter.JobLevel) || model.job_level == filter.JobLevel))
            &&
            (string.IsNullOrEmpty(filter.PhoneNumber) || model.phone_number == filter.PhoneNumber);
        }

        public async Task<EmployeeEntity> Save(EmployeeEntity employeeEntity)
        {
            /*if (employeeEntity.Id == Guid.Empty)
                employeeEntity.Id= Guid.NewGuid(); */

            var model = _mapper.Map<EmployeeDataModel>(employeeEntity);
            var modelSaved = await _dapperDataSource.Save(model);

            return _mapper.Map<EmployeeEntity>(modelSaved);
        }
        
        private async Task LoadManager(EmployeeEntity entity)
        {
            if (entity?.ManagerId > 0)
            {
                var manager = await Load(entity.ManagerId.Value);

                entity.AssignManager(manager);
            }
        }
        
    }
}