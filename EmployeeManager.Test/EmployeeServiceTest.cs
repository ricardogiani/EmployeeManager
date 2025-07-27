using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Exceptions;
using EmployeeManager.Domain.Interfaces.Repositories;
using EmployeeManager.Domain.Requests;
using EmployeeManager.Domain.Services;
using Moq;

namespace EmployeeManager.Test
{
    [TestClass]
    public class EmployeeServiceTest
    {
        [TestMethod]
        public async Task CreateEmployee_ShouldBusinessExceptions()
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
                    employee.BirthDate = DateTime.Now.Date.AddYears(-19);

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToCreate = faker.Generate();
            employeeToCreate.JobLevel = Domain.Enums.JobLevelEnum.Director;

            var repoMock = new Mock<IEmployeeRepository>();

            repoMock.Setup(repo => repo.Query(new EmployeeFilterRequest()))
                    .ReturnsAsync([]);

            repoMock.Setup(repo => repo.Save(employeeToCreate))
                      .ReturnsAsync(employeeToCreate);

            EmployeeService service = new EmployeeService(repoMock.Object);

            await Assert.ThrowsExceptionAsync<BusinessRuleValidationException>(async () =>
                {
                    await service.Create(employeeToCreate, string.Empty);
                }
            );

        }

        [TestMethod]
        public async Task CreateEmployee_ShouldReturnEmployee_WhenCreated()
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
                    employee.BirthDate = DateTime.Now.Date.AddYears(-19);

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToCreate = faker.Generate();

            employeeToCreate.JobLevel = Domain.Enums.JobLevelEnum.Intern;

            var repoMock = new Mock<IEmployeeRepository>();

            repoMock.Setup(repo => repo.Query(new EmployeeFilterRequest()))
                    .ReturnsAsync([]);

            repoMock.Setup(repo => repo.Save(employeeToCreate))
                      .ReturnsAsync(employeeToCreate);

            EmployeeService service = new EmployeeService(repoMock.Object);

            var newEmployeeCreated = await service.Create(employeeToCreate, string.Empty);

            Assert.IsTrue(newEmployeeCreated != null, "Employe created");

        }

        [TestMethod]
        public async Task GetEmployee_GetById_ShouldReturnOneEmployee()
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
                    employee.BirthDate = f.Person.DateOfBirth;
                    employee.Id = f.Person.Random.Int();

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToFind = faker.Generate();

            var repoMock = new Mock<IEmployeeRepository>();

            int id_to_find = employeeToFind.Id;
            repoMock.Setup(repo => repo.Load(id_to_find))
                      .ReturnsAsync(employeeToFind);

            // TODO simular os metodos do repoMock
            EmployeeService service = new EmployeeService(repoMock.Object);

            var remployeeResult = await service.GetById(id_to_find);

            Assert.IsTrue(remployeeResult?.Id == id_to_find, "Employee returned");
        }

        [TestMethod]
        public async Task GetEmployees_Get_ShouldReturnManyEmployees()
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
                    employee.BirthDate = f.Person.DateOfBirth;

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            IEnumerable<EmployeeEntity> employees = faker.Generate(10);

            var requestFilter = new EmployeeFilterRequest();

            var repoMock = new Mock<IEmployeeRepository>();
            repoMock.Setup(repo => repo.Query(requestFilter))
                      .ReturnsAsync(employees);

            EmployeeService service = new EmployeeService(repoMock.Object);

            var remployeeResult = await service.Get(requestFilter);

            Assert.IsTrue(remployeeResult?.Count() == employees.Count(), "Employee Query Ok");
        }

        [TestMethod]
        public async Task CreateEmployee_ShouldExceptions_NewEmployeeIsNotAdult()
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
                    employee.BirthDate = DateTime.Now.Date.AddYears(-17); // Not adult

                    return employee;
                });

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToCreate = faker.Generate();

            Console.WriteLine($"Data nascimento: {employeeToCreate.BirthDate}");

            employeeToCreate.JobLevel = Domain.Enums.JobLevelEnum.Intern;

            var repoMock = new Mock<IEmployeeRepository>();

            repoMock.Setup(repo => repo.Query(new EmployeeFilterRequest()))
                    .ReturnsAsync([]);

            repoMock.Setup(repo => repo.Save(employeeToCreate))
                      .ReturnsAsync(employeeToCreate);

            EmployeeService service = new EmployeeService(repoMock.Object);

            var exception = await Assert.ThrowsExceptionAsync<BusinessRuleValidationException>(async () =>
                {
                    var newEmployeeCreated = await service.Create(employeeToCreate, string.Empty);
                }, "Assert BusinessRuleValidationException Ok"
            );

            Assert.IsTrue(exception.Errors.Where(message => (message == BusinessRuleValidationMessages.MessageBirthDate)).Any());

        }

        [TestMethod]
        public async Task CreateEmployee_ShouldExceptions_InvalidPassword()
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
                    employee.BirthDate = DateTime.Now.Date.AddYears(-17); // Not adult

                    return employee;
                });

            var newInvalidPassword = "12345678";

            // user analyst
            EmployeeEntity user = faker.Generate();

            EmployeeEntity employeeToCreate = faker.Generate();

            Console.WriteLine($"Data nascimento: {employeeToCreate.BirthDate}");

            employeeToCreate.JobLevel = Domain.Enums.JobLevelEnum.Intern;

            var repoMock = new Mock<IEmployeeRepository>();

            repoMock.Setup(repo => repo.Query(new EmployeeFilterRequest()))
                    .ReturnsAsync([]);

            repoMock.Setup(repo => repo.Save(employeeToCreate))
                      .ReturnsAsync(employeeToCreate);

            EmployeeService service = new EmployeeService(repoMock.Object);

            var exception = await Assert.ThrowsExceptionAsync<BusinessRuleValidationException>(async () =>
                {
                    var newEmployeeCreated = await service.Create(employeeToCreate, newInvalidPassword);
                }, "Assert InvalidPasswordException Ok"
            );
        }
    }
}