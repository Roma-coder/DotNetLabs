using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class Option 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name ="Назва")]
        public string Title { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

   
        public override string ToString()
        {
            return $"Id: {Id}; Title: {Title}; Description: {Description}; Price: {Price}";
        }
    }
}
