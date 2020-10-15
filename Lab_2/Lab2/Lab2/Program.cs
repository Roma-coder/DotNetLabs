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
            var data = context.Exams
                .Join(context.Students, o => o.StudentId, c => c.Id,
                    (o, c) => new { SubjectId = o.SubjectId, TeacherId = o.TeacherId, SFirstName = c.FirstName, SLastName = c.LastName })
                .Join(context.Subjects, oc => oc.SubjectId, p => p.Id,
                    (oc, p) => new { Subject = p.Name, TeacherId = oc.TeacherId, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .Join(context.Teachers, oc => oc.TeacherId, p => p.Id,
                    (oc, p) => new { Subject = oc.Subject, TFirstName = p.FirstName, TLastName = p.LastName, SFirstName = oc.SFirstName, SLastName = oc.SLastName })
                .ToList()
                .GroupBy(t => new { t.SFirstName, t.SLastName })
                .Where(g => g.Count() >= 2);

            foreach (var el in data)
            {
                string[] subjects = el.Select(q => q.Subject).ToArray();
                string[] teachers = el.Select(q => q.TLastName + " " + q.TFirstName).ToArray();

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
                var students = Enumerable.Cast<Student>(ImportData(context, typeof(Student), "students"));
                var subjects = Enumerable.Cast<Subject>(ImportData(context, typeof(Subject), "subjects"));
                var teachers = Enumerable.Cast<Teacher>(ImportData(context, typeof(Teacher), "teachers"));

                ImportExams(context, students, subjects, teachers);

                PrintData(context);
                DeleteData(context);
            }
        }
    }
}
