using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("challenge")]
    public class Challenge
    {
        [Key]
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("name"), Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("slug"), Required]
        [MaxLength(50)]
        public string Slug { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreateAt { get; set; }


        public ICollection<Acceleration> Accelerations { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}