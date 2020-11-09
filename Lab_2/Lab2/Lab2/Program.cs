using Lab2.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using Lab2.Interfaces;

namespace Lab2
{
    class Program
    {
        const string DataPath = "Data";

        private static List<IReadableFromString> ImportData(ApplicationContext context, Type type, string path)
        {
            var data = new List<IReadableFromString>();

            var lines = File.ReadAllLines($"{DataPath}\\{path}.txt");            
            foreach (var line in lines)
            {
                var readable = Activator.CreateInstance(type) as IReadableFromString;
                readable.ReadFromStringArray(line.Split(';'));
                data.Add(readable);
            }

            context.AddRange(data);
            context.SaveChanges();

            return data;
        }

        private static void ImportExams(ApplicationContext context,
            IEnumerable<Student> students, IEnumerable<Subject> subjects, IEnumerable<Teacher> teachers)
        {
            List<Exam> exams = new List<Exam>();

            var lines = File.ReadAllLines($"{DataPath}\\exams.txt");
            foreach (var line in lines)
            {
                var words = line.Split(';');

                Exam exam = new Exam();
                exam.StudentId = students.ElementAt(int.Parse(words[1]) - 1).Id;
                exam.SubjectId = subjects.ElementAt(int.Parse(words[2]) - 1).Id;
                exam.TeacherId = teachers.ElementAt(int.Parse(words[3]) - 1).Id;
                exam.Mark = int.Parse(words[4]);

                exams.Add(exam);
            }

            context.AddRange(exams);
            context.SaveChanges();
        }

        private static void PrintData(ApplicationContext context)
        {
            var data = context.Students
                .GroupJoin(context.Exams,
                    student => student.Id,
                    exam => exam.StudentId,
                    (x, y) => new { Student = x, Exams = y })
                .SelectMany(xy => xy.Exams.DefaultIfEmpty(), (x, y) => new { x.Student, Exam = y })
                .GroupBy(s => s.Student)
                .ToDictionary(e => e.Key, e => e.Select(x => x.Exam?.ToString()));

            Console.WriteLine("-------------------------- All Students --------------------------");
            foreach (var el in data)
            {
                string[] exams = el.Value;

                var examsStr = string.Empty;
                foreach (var exam in exams)
                {
                    examsStr += exam != null ? $"{exam}, " : "";
                }

                if (examsStr.Length > 2)
                {
                    examsStr = examsStr.Substring(0, examsStr.Length - 2);
                }
                else
                {
                    examsStr = "Does not have an exams!";
                }

                Console.WriteLine($"Student {el.Key.FirstName} {el.Key.LastName} - {examsStr}");
            }
            Console.WriteLine("\n");
        }

        private static void PrintDataFromAggregation(ApplicationContext context)
        {
            var data = context.Exams
                .Join(context.Students, o => o.StudentId, c => c.Id,
                    (o, c) => new { SubjectId = o.SubjectId, TeacherId = o.TeacherId, SFirstName = c.FirstName, SLastName = c.LastName })
                .Join(context.Subjects, oc => oc.SubjectId, p => p.Id,
                    (oc, p) => new { Subject = p.Name, TeacherId = oc.TeacherId, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .Join(context.Teachers, oc => oc.TeacherId, p => p.Id,
                    (oc, p) => new { Subject = oc.Subject, Teacher = p, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .GroupBy(t => new { t.SFirstName, t.SLastName })
                .Where(g => g.Count() >= 2)
                .Select(e => new { e.Key, Count = e.Count(), Subjects = e.Select(e => e.Subject).ToArray(), Teachers = e.Select(e => e.Teacher.FirstName + " " + e.Teacher.LastName).ToArray() })
                .ToDictionary(e => e.Key, e => new object[] { e.Subjects, e.Teachers });

            Console.WriteLine("-------------------------- Aggregated Data --------------------------");
            foreach (var el in data)
            {
                string[] subjects = (string[])el.Value[0];
                string[] teachers = (string[])el.Value[1];

                Console.WriteLine($"'{el.Key.SFirstName} {el.Key.SLastName}' " +
                    $"| Subjects [{string.Join(", ", subjects)}] " +
                    $"| Teachers [{string.Join(", ", teachers)}]"
                    );
            }
        }

        static void DeleteData(ApplicationContext context)
        {
            context.Exams.RemoveRange(context.Exams);
            context.Students.RemoveRange(context.Students);
            context.Subjects.RemoveRange(context.Subjects);
            context.Teachers.RemoveRange(context.Teachers);
            context.SaveChanges();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            using (var context = new ApplicationContext())
            {
                DeleteData(context);

                var students = Enumerable.Cast<Student>(ImportData(context, typeof(Student), "students"));
                var subjects = Enumerable.Cast<Subject>(ImportData(context, typeof(Subject), "subjects"));
                var teachers = Enumerable.Cast<Teacher>(ImportData(context, typeof(Teacher), "teachers"));

                ImportExams(context, students, subjects, teachers);

                PrintData(context);
                PrintDataFromAggregation(context);                
            }
        }
    }
}
