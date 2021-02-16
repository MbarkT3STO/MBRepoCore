using System.ComponentModel.DataAnnotations;

namespace MBRepoCore.Models_Example
{
    class Student
    {
        [Key,MaxLength(50)]
        public string ID { get;        set; }
        [MaxLength(50)]
        public string Name      { get; set; }
        [MaxLength(50)]
        public string BrancheID { get; set; }

        public Branche Branche { get; set; }
    }
}
