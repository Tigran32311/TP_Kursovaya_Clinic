using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClinicDbContext _context;

        public AccountController(ClinicDbContext clinicDbContext)
        {
            _context = clinicDbContext;
        }
        //проверка Email при регистрации
        public async Task<IActionResult> CheckEmail([Bind("Email")] Patient patient)
        {
            if (_context.patients.Any(x => x.Email == patient.Email))
            {
                return Json(false);
            }
            return Json(true);

        }
        
        public IActionResult Login([Bind ("Email,Password")] ForLogin forLogin)
        {
            if (_context.patients.Any(a=>a.Password == forLogin.Password && a.Email==forLogin.Email))
            {
                int id = _context.patients.Where(a => a.Password == forLogin.Password && a.Email == forLogin.Email).Select(a=>a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("MainPagePatient","Patient", new {id = id});
            } else if (_context.doctors.Any(a => a.Password == forLogin.Password && a.Email == forLogin.Email))
            {
                int id = _context.doctors.Where(a => a.Password == forLogin.Password && a.Email == forLogin.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("MainPageDoctor", "Doctor", new { id = id });
            }
            else if (_context.admins.Any(a => a.Password == forLogin.Password && a.Email == forLogin.Email))
            {
                int id = _context.admins.Where(a => a.Password == forLogin.Password && a.Email == forLogin.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("MainPageAdmin", "Admin", new { id = id });
            }
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("userId");
            return RedirectToAction("MainPage", "Home"); //добавить контроллер и метод для главной страницы
        }

        public IActionResult Register([Bind ("Name,Surname,Fathername,Email,Password")] Patient patient)
        {
            if (patient.Name!=null)
            {
                _context.patients.Add(patient);
                _context.SaveChanges();

                int id = _context.patients.Where(a => a.Password == patient.Password && a.Email == patient.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("MainPagePatient", "Patient", new { id = id });
            } else
            {
                return View();
            }
        }
    }
}
