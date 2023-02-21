using Microsoft.AspNetCore.Identity;

public class AccountDAO : IdentityUser<Guid>
{
    public AccountDAO() {
        this.Id=Guid.NewGuid();
        // this.Email=Email;
        // this.PasswordHash=PasswordHash;
    }

    public AppUserDAO? AppUser { get; set; }
    
}
