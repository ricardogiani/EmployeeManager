using Bogus;
using Bogus.Extensions.Brazil;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Test;

[TestClass]
public class EmployeeEntityTest
{
    [TestMethod]
    public void TestEmployeePasswordIsHardAndFailBecauseIsSoft()
    {

        var faker = new Faker<EmployeeEntity>()
            .CustomInstantiator(f => new EmployeeEntity(
                f.Person.FirstName,
                f.Person.LastName,
                f.Internet.Email(),
                f.Person.Cpf()
            ));

        EmployeeEntity employee = faker.Generate();

        string passwordSoft = "12345678";        

        Assert.ThrowsException<ArgumentException>(() => employee.GeneratePassword(passwordSoft), "Test invalid password sucess!");
    }

    [TestMethod]
    public void TestEmployeePasswordIsHardAndSucess()
    {

        var faker = new Faker<EmployeeEntity>()
            .CustomInstantiator(f => new EmployeeEntity(
                f.Person.FirstName,
                f.Person.LastName,
                f.Internet.Email(),
                f.Person.Cpf()
            ));

        EmployeeEntity employee = faker.Generate();

        string hardPassword = $"{ new Faker().Internet.Password(length:8) }Aa1*";

        employee.GeneratePassword(hardPassword);

        Assert.IsTrue(!string.IsNullOrEmpty(employee.PasswordHash), $"Test valid password sucess! {hardPassword}");

    }
}