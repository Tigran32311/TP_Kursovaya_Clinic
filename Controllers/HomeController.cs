using Clinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Controllers
{
	public class HomeController : Controller
	{
		private readonly ClinicDbContext _context;


        public HomeController(ClinicDbContext context)
		{
			_context = context;
		}

		public IActionResult MainPage()
		{
			return View();
		}

		public IActionResult InfoAboutClinic()
		{
			return View();
		}
		public IActionResult ListServices()
		{
			ViewData["Services"] = _context.services.Include(s=>s.Specialization).ToList();
			return View();
		}
		
	}
}
