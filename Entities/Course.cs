﻿namespace ITI.Entities;

public class Course
{
    public int CrsId { get; set; }

    public string? CrsName { get; set; }

    public int? CrsDuration { get; set; }

    public int? TopId { get; set; }

    public virtual ICollection<InsCourse> InsCourses { get; set; } = new List<InsCourse>();

    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();

    public virtual Topic? Top { get; set; }
}
