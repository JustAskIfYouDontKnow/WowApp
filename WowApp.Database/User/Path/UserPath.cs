namespace WowApp.Database.User.Path;

public sealed class UserPath
{
    public readonly string? FirstName;

    public readonly string? LastName;

    public readonly string? Email;

    public UserPath(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

}