using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MBRepoCore.Models
{
    [Table("Archive_Client")]
    public partial class ArchiveClient
    {
        [Key]
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nom { get; set; }
        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }
        [Column("Date_De_Naissance", TypeName = "date")]
        public DateTime DateDeNaissance { get; set; }
        [Required]
        [StringLength(50)]
        public string Ville { get; set; }
    }
}
