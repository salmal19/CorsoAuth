using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoAuth.Models
{
    public class UtenteLoginModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150,ErrorMessage = "Email troppo lunga(max 150 caratteri)")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

    }

    public class UtenteCreateModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150, ErrorMessage = "Email troppo lunga(max 150 caratteri)")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Username troppo lunga(max 20 caratteri)")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Conferma Password")]
        [Compare("Password")]
        public string RePassword { get; set; }

    }
}