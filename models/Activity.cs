using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    [Table("activity")]
    public class Activity
    {
        [Column("id")]
        public Guid Id { get; set; }
    
        [Column("activity_type")]
        public required Enums.ActivityType ActivityType { get; set; }
    
        [Column("message")]
        [MaxLength(150)]
        public required string Message { get; set; }
        
        [Column("timestamp", TypeName = "timestamp with time zone")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        
        [Column("user_id")]
        public required Guid UserId { get; set; }
        public required User User { get; set; }
    }
}

