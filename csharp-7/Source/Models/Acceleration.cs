using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    /** Classe acceleration
     FK com a Tabela de Challenge
      Collection com a Tabela de Candidate */

    [Table("acceleration")]
    public class Acceleration
    {
        [Key]
        [Column("id"), Required]
        public int Id { get; set; } 

        [Column("created_at"), Required]
        public DateTime CreateAt { get; set; }

        [Column("name"), Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("slug"), Required]
        [MaxLength(50)]
        public string Slug { get; set; }

        [Column("challenge_id"), Required]
        public int ChallengeID { get; set; }

        public Challenge Challenge { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}