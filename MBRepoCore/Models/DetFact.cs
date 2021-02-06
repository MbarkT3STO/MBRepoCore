using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Det_Fact")]
    public partial class DetFact
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("CMD")]
        [StringLength(50)]
        public string Cmd { get; set; }
        [Required]
        [Column("FACT")]
        [StringLength(50)]
        public string Fact { get; set; }

        [ForeignKey(nameof(Cmd))]
        [InverseProperty(nameof(Commande.DetFacts))]
        public virtual Commande CmdNavigation { get; set; }
        [ForeignKey(nameof(Fact))]
        [InverseProperty(nameof(Facture.DetFacts))]
        public virtual Facture FactNavigation { get; set; }
    }
}
