using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Stock")]
    public partial class Stock
    {
        [Key]
        [Column("PRD")]
        [StringLength(50)]
        public string Prd { get; set; }
        [Column("QTE")]
        public int? Qte { get; set; }

        [ForeignKey(nameof(Prd))]
        [InverseProperty(nameof(Produit.Stock))]
        public virtual Produit PrdNavigation { get; set; }
    }
}
