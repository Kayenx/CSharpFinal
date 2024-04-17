using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace csharp2proj.Models
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Toto pole je nutno vyplnit.")]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Toto pole je nutno vyplnit.")]
        [Display(Name = "Heslo")]
        public string Password { get; set; }
    }
}
