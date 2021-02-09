using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MBRepoCore.Models_Example
{
    class Branche
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Title { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
