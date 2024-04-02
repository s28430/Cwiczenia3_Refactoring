using System;

namespace LegacyApp;

public class SimpleUserValidator : IUserValidator
{
    public bool ValidateUser(User user)
    {
        return ValidateFullName(user.FirstName, user.LastName) 
               && ValidateEmail(user.EmailAddress) 
               && ValidateAgeMoreThan21(user.DateOfBirth);
    }
    
    private static bool ValidateFullName(string firstName, string lastName)
    {
        return !(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName));
    }

    private static bool ValidateEmail(string email)
    {
        return email.Contains('@') || email.Contains('.');
    }

    private static bool ValidateAgeMoreThan21(DateTime birthDate)
    {
        var now = DateTime.Now;
        var age = now.Year - birthDate.Year;
        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;

        return age >= 21;
    }
}