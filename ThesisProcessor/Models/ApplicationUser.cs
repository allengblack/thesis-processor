using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ThesisProcessor.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Thesis Thesis { get; set; }
    }
}
