using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace csharp2proj.Models
{
    public class RegistrationForm
    {
        [Required(ErrorMessage = "Toto pole je nutno vyplnit.")]
        [Display(Name = "Křestní jméno")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Toto pole je nutno vyplnit.")]
        [Display(Name = "Přijmení")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Toto pole je nutno vyplnit.")]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Toto pole je nutno vyplnit.")]
        [Display(Name = "Heslo")]
        public string Password { get; set; }
    }
}
