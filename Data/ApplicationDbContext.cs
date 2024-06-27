using ITI.Entities;
using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace ITI.ReverseEngineering.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<InsCourse> InsCourses { get; set; }
    public virtual DbSet<Instructor> Instructors { get; set; }
    public virtual DbSet<StudCourse> StudCourses { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Topic> Topics { get; set; }
    public DbSet<AlexStudentView> AlexStudents { get; set; }
    public DbSet<CairoAlexStudentGradesView> CairoAlexStudentGrades { get; set; }
    public DbSet<ManagerWithTopicView> TopicsTeachedByManagers { get; set; }
    public DbSet<EmployeeWithProjectView> EmployeesAssignedPerProject { get; set; }
    public DbSet<StudentWithAddress> StudentWithAddresses { get; set; }
    public DbSet<StudentsPerDepartment> StudentsPerDepartment { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDbFunction(
            typeof(ApplicationDbContext)
            .GetMethod(nameof(GetDepartmentInstructorsByDeptId), [typeof(int)])!);
    }

    [DbFunction("GetStudentNameByStudentId", Schema = "dbo")]
    public static string GetStudentNameBy(int studentId)
        => throw new NotImplementedException();

    public IQueryable<Instructor> GetDepartmentInstructorsByDeptId(int departmentId)
        => FromExpression(() => GetDepartmentInstructorsByDeptId(departmentId));
}
