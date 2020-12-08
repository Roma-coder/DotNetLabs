using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Client 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Прізвище")]
        public string Name { get; set; }
        [Display(Name = "Ім'я")]
        public string Surname { get; set; }
        [Display(Name = "По-батькові")]
        public string MiddleName { get; set; }
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Display(Name = "Номер телефону")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual  ICollection<Order> Orders { get; set; }

        public string FullName => $"{Name} {Surname} {MiddleName}";

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Surname: {Surname}; MiddleName: {MiddleName}; Address: {Address}; PhoneNumber: {PhoneNumber};";
        }

       
        
    }
}
