using ITI.Helpers;
using ITI.ReverseEngineering.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetSection("constr").Value;

        ServiceCollection services = new();
        services.AddDbContextPool<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        context.CountStudentsForEachSupervisor();
        context.CountStudentsWhoHaveValueInTheirAge();
        context.ListStudentsWithHisSupervisorsData();
        context.NumberOfCoursesForEachTopic();
        context.StudentsWithDepartmentAccordingToSpecificFormat();
        context.SumSalaryForInstructorsInEachDepartment();
        context.Get5thOlderStudentUseRanking();
        context.Get5thYoungerStudentUseDatabaseRanking();
        context.Get5thOlderStudentRankingByFluentSyntax();
        context.SelectAllStudentsWhereAgeGreatherThanAverageOfAllStudents();
        context.GetAllDepartmentsThatContainsStudentsUsingSubQuery();
        context.GetStudentsLivesInCairoUsingJoin();
        context.DeleteAllStudentsWithLivesInCairoUsingJoin();
        context.GetYoungestStudentInEachDepartmentUsingPartitionWithRanking();

        Console.ReadKey();
    }
}