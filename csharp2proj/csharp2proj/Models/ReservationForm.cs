using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace csharp2proj.Models
{
    public class ReservationForm
    {
        [Required(ErrorMessage = "Zadejte prosím datum začátku rezervace.")]
        [DataType(DataType.DateTime, ErrorMessage = "Špatný formát, použijte prosím formát dd.MM.yyyy.")]
        [Display(Name = "Začátek rezervace")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Zadejte prosím datum konce rezervace.")]
        [DataType(DataType.DateTime, ErrorMessage = "Špatný formát, použijte prosím formát dd.MM.yyyy.")]
        [Display(Name = "Konec rezervace")]
        public DateTime End { get; set; }
    }
}
