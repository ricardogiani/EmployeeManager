using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Exceptions;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Services;
using Moq;

namespace EmployeeManager.Test
{
    [TestClass]
    public class EmployeeServiceTest
    {
        [TestMethod]
        public void TestCreateEmployeeAndFailBecauseNewEmployeeHasHigherLevel()
        {
            var faker = new Faker<EmployeeEntity>()
                .CustomInstantiator(f =>
                {
                    var employee = new EmployeeEntity(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Internet.Email(),
                        f.Person.Cpf()
                    );

                    employee.JobLevel = Domain.Enums.JobLevelEnum.Analyst;

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToCreate = faker.Generate();
            employeeToCreate.JobLevel = Domain.Enums.JobLevelEnum.Coordinator;


            Assert.ThrowsException<BusinessRuleValidationException>(() =>
                {
                    if ((int)user.JobLevel <= (int)employeeToCreate.JobLevel)
                    {
                        throw new BusinessRuleValidationException("Operation not permited");
                    }
                }
            );
            // Mock do repositório (ajuste conforme sua interface)
            /*var repoMock = new Mock<IEmployeeRepository>();
            var service = new EmployeeService(repoMock.Object);

            // Testa se lança exceção ao tentar criar funcionário com nível superior
            */

        }

        [TestMethod]
        public async Task TestCreateEmployeeAndSucess()
        {
            var faker = new Faker<EmployeeEntity>()
                .CustomInstantiator(f =>
                {
                    var employee = new EmployeeEntity(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Internet.Email(),
                        f.Person.Cpf()
                    );

                    employee.JobLevel = Domain.Enums.JobLevelEnum.Analyst;

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToCreate = faker.Generate();
            employeeToCreate.JobLevel = Domain.Enums.JobLevelEnum.Intern;

            var repoMock = new Mock<IEmployeeRepository>();
            
            // TODO simular os metodos do repoMock
            EmployeeService service = new EmployeeService(repoMock.Object);

            var newEmployeeCreated = await service.Create(employeeToCreate);

            var createdAt = newEmployeeCreated.CreatedAt;
            Assert.IsTrue(createdAt.Value.Date == DateTime.Now.Date, "Employe created" );

        }
    }
}