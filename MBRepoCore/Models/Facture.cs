using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Facture")]
    public partial class Facture
    {
        public Facture()
        {
            DetFacts = new HashSet<DetFact>();
            Reglements = new HashSet<Reglement>();
        }

        [Key]
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("CLT")]
        [StringLength(50)]
        public string Clt { get; set; }

        [ForeignKey(nameof(Clt))]
        [InverseProperty(nameof(Client.Factures))]
        public virtual Client CltNavigation { get; set; }
        [InverseProperty(nameof(DetFact.FactNavigation))]
        public virtual ICollection<DetFact> DetFacts { get; set; }
        [InverseProperty(nameof(Reglement.FactNavigation))]
        public virtual ICollection<Reglement> Reglements { get; set; }
    }
}
