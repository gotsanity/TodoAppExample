using Microsoft.AspNetCore.Identity;

namespace TodoApp.Data
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string PetName { get; set; }
    }
}
