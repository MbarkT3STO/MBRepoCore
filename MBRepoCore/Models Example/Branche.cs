using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MBRepoCore.Models_Example
{
    class Branche
    {
        [Key,MaxLength(50)]
        public string ID { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
