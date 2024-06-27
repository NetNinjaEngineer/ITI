using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.ReverseEngineering.DTOs
{
    public class StudentDto
    {
        [Column("St_Fname")]
        public string? FirstName { get; set; }
        [Column("St_Age")]
        public int Age { get; set; }
        [Column("Dept_Id")]
        public int DepartmentId { get; set; }
    }
}
