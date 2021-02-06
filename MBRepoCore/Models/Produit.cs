using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Produit")]
    public partial class Produit
    {
        public Produit()
        {
            Commandes = new HashSet<Commande>();
        }

        [Key]
        [Column("REF")]
        [StringLength(50)]
        public string Ref { get; set; }
        [Required]
        [StringLength(50)]
        public string Desi { get; set; }
        [Column("PRU", TypeName = "money")]
        public decimal Pru { get; set; }

        [InverseProperty("PrdNavigation")]
        public virtual Stock Stock { get; set; }
        [InverseProperty(nameof(Commande.PrdNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; }
    }
}
