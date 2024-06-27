namespace ITI.Entities;

public class Instructor
{
    public int InsId { get; set; }

    public string? InsName { get; set; }

    public string? InsDegree { get; set; }

    public decimal? Salary { get; set; }

    public int? DeptId { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Department? Dept { get; set; }

    public virtual ICollection<InsCourse> InsCourses { get; set; } = new List<InsCourse>();
}
