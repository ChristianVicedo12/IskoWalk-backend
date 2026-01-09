using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IskoWalk.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column("FullName")]
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Column("Email")]
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Column("PasswordHash")]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
