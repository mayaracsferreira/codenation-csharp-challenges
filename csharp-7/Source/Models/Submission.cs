using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Codenation.Challenge.Models
{
    [Table("submission")]
    public class Submission
    {
        [ForeignKey("User")]
        [Column("user_id")]
        [Required]
        public int UserId { get; set; }
        [ForeignKey("Challenge")]
        [Column("challenge_id")]
        [Required]
        public int ChallengeId { get; set; }
        [Column("score")]
        [Required]
        public decimal Score { get; set; }
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Challenge Challenge { get; set; }
    }
}
