using Microsoft.AspNetCore.Identity;

namespace cheez_ims_api.models
{
    public class User : IdentityUser  // Inherits default Identity fields (Id, Email, etc.)
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
