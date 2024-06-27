namespace ITI.Entities;

public class StudCourse
{
    public int CrsId { get; set; }

    public int StId { get; set; }

    public int? Grade { get; set; }

    public virtual Course Crs { get; set; } = null!;

    public virtual Student St { get; set; } = null!;
}
