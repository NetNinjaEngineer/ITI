namespace ITI.Entities;

public class Topic
{
    public int TopId { get; set; }

    public string? TopName { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
