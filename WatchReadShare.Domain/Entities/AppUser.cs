using Microsoft.AspNetCore.Identity;

namespace WatchReadShare.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
