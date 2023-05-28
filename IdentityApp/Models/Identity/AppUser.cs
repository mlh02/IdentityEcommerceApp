using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdentityEcommerce.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string AssignedCompanyId { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public int MyRewardPoints { get; set; }
    }

}
