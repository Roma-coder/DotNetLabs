using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab4.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [Display(Name = "Номер телефону")]
        public string Phone { get; set; }

        [Display(Name = "День народження")]
        public DateTime Birth { get; set; }

        [Display(Name = "Стипендія")]
        public int Studentship { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
