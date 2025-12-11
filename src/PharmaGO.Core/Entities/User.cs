using PharmaGO.Core.Entities.Base;

namespace PharmaGO.Core.Entities;

public class User : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    
    public void UpdatePassword(string hashPassword)
    {
        if (string.IsNullOrEmpty(hashPassword))
        {
            throw new ArgumentNullException(nameof(hashPassword));
        }
        
        Password = hashPassword;
    }
}