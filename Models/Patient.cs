using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fathername { get; set; }
        [EmailAddress]
        [Remote(action:"CheckEmail", controller:"Home",ErrorMessage ="Этот Email уже занят")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
