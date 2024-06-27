using ITI.ReverseEngineering.Data;
using ITI.ReverseEngineering.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ITI.Helpers
{
    public static class DatabaseHelper
    {
        public static void CountStudentsForEachSupervisor(this ApplicationDbContext context)
        {
            /*

                -- COUNT OF STUDENTS FOR EACH SUPERVISOR

                SELECT CONCAT(SuperVisor.St_Fname, ' ', SuperVisor.St_Lname) AS 'SuperVisor',
                COUNT(S.St_Id) AS '#Students'
                FROM Student S INNER JOIN Student SuperVisor
                ON SuperVisor.St_Id = S.St_super
                GROUP BY SuperVisor.St_Id, SuperVisor.St_Fname, SuperVisor.St_Lname;

             */

            var query = from student in context.Students
                        join superVisor in context.Students
                        on student.StSuper equals superVisor.StId
                        group new { student, superVisor }
                            by new { superVisor.StId, superVisor.StFname, superVisor.StLname }
                        into grouped
                        select new
                        {
                            SuperVisor = string.Concat(grouped.Key.StFname, " ", grouped.Key.StLname),
                            NumOfSupervisedStudents = grouped.Count(x => x.student != null),
                            Students = grouped.Select(x => string.Concat(x.student.StFname, " ", x.student.StLname))
                        };

            foreach (var item in query)
                Console.WriteLine($"" +
                    $"\nSuperVisor: {item.SuperVisor}" +
                    $"\nNumOfSupervisedStudents: {item.NumOfSupervisedStudents}" +
                    $"\nStudents: {string.Join("|", item.Students)}");
        }

        public static void CountStudentsWhoHaveValueInTheirAge(this ApplicationDbContext context)
        {

            /*

                -- 1.	Retrieve a number of students who have a value in their age
                SELECT COUNT(S.St_Id) FROM Student S WHERE S.St_Age IS NOT NULL;

             */

            var numOfStudentsWhoHaveValueInAAge = context.Students.Count(s => s.StAge != null);
            Console.WriteLine(numOfStudentsWhoHaveValueInAAge);
        }

        public static void ListStudentsWithHisSupervisorsData(this ApplicationDbContext context)
        {

            /*

                -- 3.	Select Student first name and the data of his supervisor 
                SELECT S.St_Fname AS 'Student Fname',
                SuperVisor.St_Id, SuperVisor.St_Fname, SuperVisor.St_Lname,
                SuperVisor.St_Address, SuperVisor.St_Age, SuperVisor.Dept_Id, SuperVisor.St_super
                FROM Student S INNER JOIN Student SuperVisor
                ON S.St_super = SuperVisor.St_Id


             */

            var studentWithHisSuperVisor = context.Students.Join(
                    context.Students,
                    student => student.StSuper,
                    superVisor => superVisor.StId,
                    (student, superVisor) => new { student, superVisor }
                ).Select(x => new
                {
                    StudentFirstName = x.student.StFname,
                    SuperId = x.superVisor.StId,
                    SuperFirstName = x.superVisor.StFname,
                    SuperLastName = x.superVisor.StLname,
                    SuperAddress = x.superVisor.StAddress,
                    SuperAge = x.superVisor.StAge,
                    SuperDeptId = x.superVisor.DeptId
                });

            foreach (var item in studentWithHisSuperVisor)
                Console.WriteLine(
                    $"\nStudentFirstName: {item.StudentFirstName}" +
                    $"\nSuperId: {item.SuperId}" +
                    $"\nSuperFirstName: {item.SuperFirstName}" +
                    $"\nSuperLastName: {item.SuperLastName}" +
                    $"\nSuperAddress: {item.SuperAddress}" +
                    $"\nSuperAge: {item.SuperAge}" +
                    $"\nSuperDeptId: {item.SuperDeptId}");
        }

        public static void NumberOfCoursesForEachTopic(this ApplicationDbContext context)
        {
            /*

                -- 2.	Display number of courses for each topic name
                SELECT T.Top_Name AS 'Topic' , COUNT(C.Crs_Id) AS 'NumOfCourses'
                FROM Course C INNER JOIN Topic T
                ON C.Top_Id = T.Top_Id
                GROUP BY C.Top_Id, T.Top_Name;


             */

            var NumCoursesForEachTopic = context.Courses.Join(
                    context.Topics,
                    course => course.TopId,
                    topic => topic.TopId,
                    (course, topic) => new { course, topic }
                ).GroupBy(
                    x => new { x.course.TopId, x.topic.TopName },
                    (key, result) => new
                    {
                        Topic = key.TopName,
                        NumOfCourses = result.Select(x => x.course).Count()
                    });

            foreach (var item in NumCoursesForEachTopic)
                Console.WriteLine($"Topic: {item.Topic}" +
                    $"\nNumOfCourses: {item.NumOfCourses}");
        }

        public static void StudentsWithDepartmentAccordingToSpecificFormat(this ApplicationDbContext context)
        {
            /*

                -- 4.	Display student with the following Format (use isNull function)
                SELECT S.St_Id AS 'Student ID',
                CONCAT(S.St_Fname, ' ', ISNULL(S.St_Lname, 'NOT FOUND')) AS 'Student Full Name',
                D.Dept_Name AS 'Department'
                FROM Student S INNER JOIN Department D
                ON S.Dept_Id = D.Dept_Id;

             */

            var query = from student in context.Students
                         join department in context.Departments
                         on student.DeptId equals department.DeptId
                         select new
                         {
                             StudentID = student.StId,
                             StudentFullName = string.Concat(student.StFname, " ", student.StLname ?? "NOT FOUND"),
                             Department = department.DeptName
                         };

            foreach (var item in query)
                Console.WriteLine(
                    $"\nStudentID: {item.StudentID}" +
                    $"\nStudentFullName: {item.StudentFullName}" +
                    $"\nDepartment: {item.Department}");
        }

        public static void SumSalaryForInstructorsInEachDepartment(this ApplicationDbContext context)
        {
            /*

            SELECT D.Dept_Id , 
            D.Dept_Name AS 'Department', 
            SUM(S.Salary) AS 'Total',
            COUNT(S.Ins_Id) AS '#Instructors',
            STRING_AGG(S.Ins_Name, ', ') AS 'Instructors'
            FROM Department D INNER JOIN Instructor S
            ON S.Dept_Id = D.Dept_Id
            GROUP BY D.Dept_Id , D.Dept_Name
            HAVING COUNT(S.Ins_Id) > 1;

             */

            var query = from department in context.Departments
                        join instructor in context.Instructors
                        on department.DeptId equals instructor.DeptId
                        group new { department, instructor } by new { department.DeptId, department.DeptName }
                        into deptGroup
                        where deptGroup.Select(x => x.instructor.InsId).Count() > 1
                        select new
                        {
                            DepartmentId = deptGroup.Key.DeptId,
                            Department = deptGroup.Key.DeptName,
                            Total = deptGroup.Sum(x => x.instructor.Salary),
                            NumInstructors = deptGroup.Select(x => x.department).Count(),
                            Instructors = string.Join(" | ", deptGroup.Select(x => x.instructor.InsName))
                        };

            foreach (var item in query)
                Console.WriteLine($"" +
                    $"\nDepartmentId: {item.DepartmentId}" +
                    $"\nDepartment: {item.Department}" +
                    $"\nTotal: {item.Total!.Value:C}" +
                    $"\nNumInstructors: {item.NumInstructors} (s)" +
                    $"\nInstructors: {item.Instructors}");


            Console.WriteLine(query.ToQueryString());
        }

        public static void Get5thOlderStudentUseRanking(this ApplicationDbContext context)
        {
            var sqlQuery = @"
                SELECT *
                FROM (
	                SELECT St_Fname,
	                St_Age,
	                Dept_Id,
	                ROW_NUMBER() OVER (ORDER BY St_Age DESC) AS RN 
	                FROM Student) 
                AS NEWTABLE
                WHERE RN = 5
            ";

            var fluentSyntax = context.Students
                .OrderByDescending(s => s.StAge)
                .AsEnumerable()
                .Select((student, index) => new
                {
                    Student = student,
                    Rank = index + 1
                })
                .FirstOrDefault(x => x.Rank == 5);


            var result = context.Database
                .SqlQueryRaw<StudentDto>(sqlQuery)
                .FirstOrDefault();

            if (fluentSyntax is not null)
                Console.WriteLine($"FirstName: {fluentSyntax.Student.StFname}, Age: {fluentSyntax.Student.StAge}, Department: {fluentSyntax.Student.DeptId}");
            else
                Console.WriteLine("No student found with row number 5.");
        }

        public static void Get5thYoungerStudentUseDatabaseRanking(this ApplicationDbContext context)
        {
            var sqlQuery = @"
                SELECT * FROM (
                    SELECT St_Fname, St_Lname, St_Age, Dept_Id, ROW_NUMBER() OVER (ORDER BY St_Age) AS RN  
                    FROM Student S WHERE St_Age IS NOT NULL
                ) AS NEWTABLE
                WHERE RN = 5
            ";

            var result = context.Database.SqlQueryRaw<StudentDto>(sqlQuery).FirstOrDefault();
            if (result is not null)
                Console.WriteLine($"FirstName: {result.FirstName}, Age: {result.Age}, Department: {result.DepartmentId}");
            else
                Console.WriteLine("No student found with row number 5.");
        }

        public static void Get5thOlderStudentRankingByFluentSyntax(this ApplicationDbContext context)
        {
            var query = context.Students
                .OrderByDescending(x => x.StAge)
                .AsEnumerable()
                .Select((student, index) => new
                {
                    Student = student,
                    Rank = index + 1
                })
                .FirstOrDefault(x => x.Rank == 5);

            if (query is not null)
                Console.WriteLine($"FirstName: {query.Student.StFname}, Age: {query.Student.StAge}, Department: {query.Student.DeptId}");
            else
                Console.WriteLine("No student found with row number 5.");
        }

        public static void SelectAllStudentsWhereAgeGreatherThanAverageOfAllStudents(this ApplicationDbContext context)
        {
            var query = context.Students.Where(x => x.StAge > context.Students.Average(x => x.StAge));

            foreach (var item in query)
                Console.WriteLine(item);
        }

        public static void GetAllDepartmentsThatContainsStudentsUsingSubQuery(this ApplicationDbContext context)
        {
            var departments = context.Departments
                .Where(x =>
                    context.Students
                    .Where(x => x.DeptId != null)
                    .Select(x => x.DeptId).Contains(x.DeptId))
                .Select(x => x.DeptName)
                .ToList();

            departments.ForEach(Console.WriteLine);

        }

        public static void DeleteAllStudentsWithLivesInCairoUsingSubQuery(this ApplicationDbContext context)
        {
            context.StudCourses
                .Where(
                    x => context.Students
                    .Where(x => x.StAddress == "Cairo")
                    .Select(x => x.StId)
                    .Contains(x.StId)).ExecuteDelete();

        }

        public static void GetStudentsLivesInCairoUsingJoin(this ApplicationDbContext context)
        {
            var studentsWithGrades = context.Students
                .Where(x => x.StAddress == "Cairo")
                .Join(context.StudCourses,
                    student => student.StId,
                    studentCourse => studentCourse.StId,
                    (student, studentCourse) => new { student, studentCourse }
                ).Select(x => new
                {
                    StudentFullname = string.Concat(x.student.StFname, " ", x.student.StLname),
                    Address = x.student.StAddress,
                    StudentGrade = x.studentCourse.Grade
                });

            Console.WriteLine($"{"Full Name",-30}{"Address",-30}{"Age",-5}");

            foreach (var student in studentsWithGrades)
                Console.WriteLine($"{student.StudentFullname} {student.Address,-20}{student.StudentGrade,-30}");
        }

        public static void DeleteAllStudentsWithLivesInCairoUsingJoin(this ApplicationDbContext context)
        {
            /*
                SELECT SC.Grade, S.St_Address
                FROM Student S INNER JOIN Stud_Course SC
                ON S.St_Id = SC.St_Id
                WHERE S.St_Address = 'Cairo';
             */

            var recordsToDelete = context.Students
                .Where(x => x.StAddress == "Cairo")
                .Join(context.StudCourses,
                    student => student.StId,
                    studentCourse => studentCourse.StId,
                    (student, studentCourse) => new { student, studentCourse }
                ).ToList();

            if (recordsToDelete.Count != 0)
            {
                recordsToDelete.ForEach(record =>
                {
                    var studentCourse =
                        context.StudCourses
                        .FirstOrDefault(x
                            => x.StId == record.student.StId
                                && x.Grade == record.studentCourse.Grade
                                && x.CrsId == record.studentCourse.CrsId);
                    if (studentCourse is not null)
                    {
                        context.StudCourses.Remove(studentCourse);
                        context.SaveChanges();
                    }

                });
            }

        }

        public static void GetYoungestStudentInEachDepartmentUsingPartitionWithRanking(this ApplicationDbContext context)
        {
            var sqlQuery = @"
                SELECT *
                    FROM (
	                    SELECT S.Dept_Id, S.St_Fname, S.St_Age,
	                    ROW_NUMBER() OVER(PARTITION BY S.Dept_Id ORDER BY S.St_Age) AS RN
	                    FROM Student S
	                    WHERE S.St_Age IS NOT NULL AND S.Dept_Id IS NOT NULL
                    ) AS NEWTABLE
                    WHERE RN = 1";

            var result = context.Students
                .AsEnumerable()
                .Where(x => x.StAge != null && x.DeptId != null)
                .OrderBy(x => x.StAge)
                .GroupBy(x => x.DeptId)
                .SelectMany(g =>
                    g.Select((student, index) => new
                    {
                        Student = student,
                        Rank = index + 1,
                    }))
                .Where(x => x.Rank == 1)
                .Select(x => new
                {
                    DepartmentId = x.Student.DeptId,
                    Student = string.Concat(x.Student.StFname, " ", x.Student.StLname),
                    Age = x.Student.StAge,
                    StudentRank = x.Rank,
                })
                .ToList();

            foreach (var item in result)
                Console.WriteLine(
                    $"Student: {item.Student}," +
                    $" Department: {item.DepartmentId}," +
                    $" Age: {item.Age}," +
                    $" Rank: {item.StudentRank}");
        }
    }
}