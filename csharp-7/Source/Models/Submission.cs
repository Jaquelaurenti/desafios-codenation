using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    /* Classe Submission, recebe FK de User e Challenge */
    [Table("submission")]
    public class Submission
    {
        [Key]
        [Column("id"), Required]
        public int Id { get; set; }

        [Column("user_id"), Required]
        public int UserId { get; set; }
        [ForeignKey("UserId"), Required]
        public virtual User User { get; set; }

        [Column("challenge_id"), Required]
        public int ChallengeId { get; set; }
        [ForeignKey("ChallengeId"), Required]
        public virtual Challenge Challenge { get; set; }

        [Column("score"), Required]
        public decimal Score { get; set; }

        [Column("created_at"), Required]
        public DateTime CreatedAt { get; set; }
    }
}