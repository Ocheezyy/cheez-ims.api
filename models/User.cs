using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace cheez_ims_api.models
{
    [Table("users")]
    public class User  // Inherits default Identity fields (Id, Email, etc.)
    {
        [Column("id")]
        public required Guid Id { get; set; }
        
        [Column("first_name")]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        
        [Column("last_name")]
        [MaxLength(50)]
        public string? LastName { get; set; }
        
        [Column("email")]
        [MaxLength(50)]
        public required string Email { get; set; }
        
        [Column("username")]
        [MaxLength(60)]
        public string? UserName { get; set; }
        
        [Column("phone")]
        [MaxLength(30)]
        public required string Phone { get; set; }
    }
}
