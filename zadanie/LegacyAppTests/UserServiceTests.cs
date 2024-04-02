using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        // arrange
        var firstName = "Jan";
        var lastName = "Malewski";
        var email = "invalidemail";
        var birthDate = new DateTime(1990, 01, 01);
        var clientId = 1;
        var service = new UserService();
        
        // act
        var result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        // assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_First_Name_Is_Empty()
    {
        // arrange
        var firstName = "";
        var lastName = "Kowalski";
        var email = "email@example.com";
        var birthDate = new DateTime(1990, 01, 01);
        var clientId = 1;
        var service = new UserService();
        
        // act
        var result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        // assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Last_Name_Is_Empty()
    {
        // arrange
        var firstName = "Jan";
        var lastName = "";
        var email = "email@example.com";
        var birthDate = new DateTime(1990, 01, 01);
        var clientId = 1;
        var service = new UserService();
        
        // act
        var result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        // assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Less_Than_21()
    {
        // arrange
        var firstName = "Jan";
        var lastName = "Malewski";
        var email = "email@example.com";
        var birthDate = new DateTime(DateTime.Now.Year - 18, 01, 01);
        var clientId = 1;
        var service = new UserService();
        
        // act
        var result = service.AddUser(firstName, lastName, email, birthDate, clientId);
        
        // assert
        Assert.False(result);
    }

    [Fact]
    public void Add_User_Should_Return_False_When_Important_Client_Has_Credit_Limit_Less_Than_500()
    {
        // arrange
        var firstName = "Jan";
        var lastName = "Kowalski";
        var email = "email@example.com";
        var birthDate = new DateTime(1990, 01, 01);
        var clientId = 1;
        var service = new UserService();
        
        // act
        var result = service.AddUser(firstName, lastName, email, birthDate, clientId);
        
        // assert
        Assert.False(result);
    }
}