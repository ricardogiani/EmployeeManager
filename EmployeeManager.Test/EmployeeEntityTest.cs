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

        Assert.ThrowsException<InvalidPasswordException>(() => employee.GeneratePassword(passwordSoft), "Test invalid password sucess!");
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

        string hardPassword = $"{new Faker().Internet.Password(length: 8)}Aa1*";

        employee.GeneratePassword(hardPassword);

        Assert.IsTrue(!string.IsNullOrEmpty(employee.PasswordHash), $"Test valid password sucess! {hardPassword}");

    }

    [TestMethod]
    public void ValidatePassword_Should_Authenticate_Sucess()
    {

        var faker = new Faker<EmployeeEntity>()
            .CustomInstantiator(f => new EmployeeEntity(
                f.Person.FirstName,
                f.Person.LastName,
                f.Internet.Email(),
                f.Person.Cpf()
            ));

        EmployeeEntity employee = faker.Generate();

        string hardPassword = $"{new Faker().Internet.Password(length: 8)}Aa1*";

        employee.GeneratePassword(hardPassword);

        Assert.IsTrue(employee.AuthenticatePassword(hardPassword), "Passeord Authenticated Sucess");

    }
    
    [TestMethod]
    public void ValidatePassword_Should_Authenticate_Fail()
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

        string hardPasswordFail = $"{ new Faker().Internet.Password(length:8) }Aa11*";

        Assert.IsFalse(employee.AuthenticatePassword(hardPasswordFail), "Invalid Password Sucess");

    }
}