using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Domian.Entities
{
    [Table("Teacher")]
    public class Teacher
    {
        public string Name { get; set; }
    }
}
