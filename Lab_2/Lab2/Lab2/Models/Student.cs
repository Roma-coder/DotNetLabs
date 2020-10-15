using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab2.Models
{
    class Student : IReadableFromString
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string LastName { get; set; }

        public string FirstName { get; set; }        

        public string MiddleName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime Birth { get; set; }

        public int Studentship { get; set; }

        [InverseProperty("Student")]
        public virtual ICollection<Exam> Exams { get; set; }

        public void ReadFromStringArray(string[] values)
        {
            LastName = values[1];
            FirstName = values[2];
            MiddleName = values[3];
            Address = values[4];
            Phone = values[5];
            Birth = DateTime.Parse(values[6]);
            Studentship = int.Parse(values[7]);
        }
    }
}
