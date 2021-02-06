using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Reglement")]
    public partial class Reglement
    {
        [Key]
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("FACT")]
        [StringLength(50)]
        public string Fact { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "money")]
        public decimal Montant { get; set; }
        [Required]
        [Column("Type_Reg")]
        [StringLength(50)]
        public string TypeReg { get; set; }

        [ForeignKey(nameof(Fact))]
        [InverseProperty(nameof(Facture.Reglements))]
        public virtual Facture FactNavigation { get; set; }
    }
}
