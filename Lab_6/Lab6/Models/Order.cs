using System;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Display(Name = "Клієнт")]
        public Guid ClientId { get; set; }
        [Display(Name = "Клієнт")]
        public Client Client { get; set; }

        [Display(Name = "Послуга")]
        public Guid OptionId { get; set; }
        [Display(Name = "Послуга")]
        public Option Option { get; set; }

        [Display(Name = "Кількість")]
        public int Quantity { get; set; }

        [Display(Name = "Дата завмолвення")]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [Display(Name = "Дата закінчення роботи")]
        [DataType(DataType.Date)]
        public DateTime DateFinish { get; set; }


        public override string ToString()
        {
            return $"Id: {Id}; ClientId: {ClientId}; OptionId: {OptionId}; DateStart: {DateStart.ToShortDateString()}; " +
                $"DateFinish: {DateFinish.ToShortDateString()};";
        }


    }
}
