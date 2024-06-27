namespace ITI.Entities;

public class Student
{
    public int StId { get; set; }

    public string? StFname { get; set; }

    public string? StLname { get; set; }

    public string? StAddress { get; set; }

    public int? StAge { get; set; }

    public int? DeptId { get; set; }

    public int? StSuper { get; set; }

    public virtual Department? Dept { get; set; }

    public virtual ICollection<Student> InverseStSuperNavigation { get; set; } = new List<Student>();

    public virtual Student? StSuperNavigation { get; set; }

    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();

    public override string ToString()
    {
        return $"Student ID: {StId}, " +
               $"First Name: {StFname ?? "N/A"}, " +
               $"Last Name: {StLname ?? "N/A"}, " +
               $"Address: {StAddress ?? "N/A"}, " +
               $"Age: {StAge?.ToString() ?? "N/A"}, " +
               $"Department ID: {DeptId?.ToString() ?? "N/A"}, " +
               $"Supervisor ID: {StSuper?.ToString() ?? "N/A"}, " +
               $"Department: {Dept?.DeptName ?? "N/A"}";
    }

}
