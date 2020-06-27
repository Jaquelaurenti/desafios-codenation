using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    /** Classe candidate
    FK com a Tabela de User, Acceleration e Company */

    [Table("candidate")]
    public class Candidate
    {
        [Key]
        [Column("user_id"), Required]
        public int UserId { get; set; }
        [ForeignKey("UserId"), Required]
        public virtual User User { get; set; }

        [Column("acceleration_id"), Required]
        public int AccelerationId { get; set; }
        [ForeignKey("AccelerationId"), Required]
        public virtual Acceleration Acceleration { get; set; }

        [Column("company_id"), Required]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId"), Required]
        public virtual Company Company { get; set; }

        [Column("status"), Required]
        public int Status { get; set; }

        [Column("created_at"), Required]        
        public DateTime CreatedAt { get; set; }
    }
}