using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Codenation.Challenge.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        [Column("full_name")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string FullName { get; set; }
        [Column("email")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("nickname")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Nickname { get; set; }
        [Column("password")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string Password { get; set; }
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
