using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Domain.Entities
{
    [Table("Student")]
    public class Student
    {
        public string Name { get; set; }
    }
}
