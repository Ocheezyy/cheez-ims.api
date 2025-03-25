using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cheez_ims_api.models
{
    public class User : IdentityUser  // Inherits default Identity fields (Id, Email, etc.)
    {
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
    }
}
