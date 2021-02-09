using System.ComponentModel.DataAnnotations;

namespace MBRepoCore.Models_Example
{
    class Student
    {
        [Key]
        public string ID { get;        set; }
        public string Name      { get; set; }
        public string BrancheID { get; set; }

        public Branche Branche { get; set; }
    }
}
