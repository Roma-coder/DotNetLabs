using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab3.Models
{
    public class Student
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
    }
}
