using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("candidate")]
    public class Candidate
    {
        [ForeignKey("User")]
        [Column("user_id")]
        [Required]
        public int UserId { get; set; }
        [ForeignKey("Acceleration")]
        [Column("acceleration_id")]
        [Required]
        public int AccelerationId { get; set; }
        [ForeignKey("Company")]
        [Column("company_id")]
        [Required]
        public int CompanyId { get; set; }
        [Column("status")]
        [Required]
        public int Status { get; set; }
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Acceleration Acceleration { get; set; }
        public virtual Company Company { get; set; }

    }
}
