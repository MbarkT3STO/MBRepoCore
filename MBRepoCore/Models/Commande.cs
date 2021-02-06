using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Commande")]
    public partial class Commande
    {
        public Commande()
        {
            DetFacts = new HashSet<DetFact>();
        }

        [Key]
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("CLT")]
        [StringLength(50)]
        public string Clt { get; set; }
        [Required]
        [Column("PRD")]
        [StringLength(50)]
        public string Prd { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("PRU", TypeName = "money")]
        public decimal Pru { get; set; }
        [Column("QTE")]
        public int Qte { get; set; }

        [ForeignKey(nameof(Clt))]
        [InverseProperty(nameof(Client.Commandes))]
        public virtual Client CltNavigation { get; set; }
        [ForeignKey(nameof(Prd))]
        [InverseProperty(nameof(Produit.Commandes))]
        public virtual Produit PrdNavigation { get; set; }
        [InverseProperty(nameof(DetFact.CmdNavigation))]
        public virtual ICollection<DetFact> DetFacts { get; set; }
    }
}
