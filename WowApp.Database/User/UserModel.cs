using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WowApp.Database.User.Path;

namespace WowApp.Database.User;

public class UserModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    
    public static UserModel CreateModel(string firstName, string lastName, string email)
    {
        return new UserModel()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }
    
    public void UpdateByUserPatch(UserPath userPath)
    {
        FirstName = !string.IsNullOrEmpty(userPath.FirstName) ? userPath.FirstName : FirstName;
        LastName  = !string.IsNullOrEmpty(userPath.LastName)  ? userPath.LastName  : LastName;
        Email     = !string.IsNullOrEmpty(userPath.Email)     ? userPath.Email     : Email;
    }
    
    public bool IsSame(UserPath userPath)
    {
        return 
            userPath.FirstName == FirstName &&
            userPath.LastName == LastName && 
            userPath.Email == Email;
    }
}