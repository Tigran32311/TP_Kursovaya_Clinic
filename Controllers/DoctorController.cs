using Microsoft.AspNetCore.Mvc;
using Clinic.Data;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ClinicDbContext _context;

        public DoctorController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult MainPageDoctor()
        {
            ViewData["Doctor"] = _context.doctors.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
            return View();
        }

        public IActionResult SuccessCreate()
        {
            return View();
        }

        public IActionResult ListAppointments()
        {
            ViewData["Appointment"] = _context.appointments.Where(a => a.DoctorId == Convert.ToInt32(Request.Cookies["userId"]) && a.Status == 1 && a.DateOfAppointment==DateTime.Today).ToList();
            ViewData["Services"] = _context.services.ToList();
            ViewData["Doctor"] = _context.doctors.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
            ViewData["Patient"] = _context.patients.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ListAppointments([Bind("DateOfAppointment")] Appointment appointment)
        {
            if (appointment.DateOfAppointment!=null && _context.appointments.Any(a => a.DoctorId == Convert.ToInt32(Request.Cookies["userId"]) && a.Status == 1 && a.DateOfAppointment == appointment.DateOfAppointment))
            {
                ViewData["Appointment"] = await _context.appointments.Where(a => a.DoctorId == Convert.ToInt32(Request.Cookies["userId"]) && a.Status == 1 && a.DateOfAppointment == appointment.DateOfAppointment).ToListAsync();
                ViewData["Services"] = _context.services.ToList();
                ViewData["Doctor"] = _context.doctors.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
                ViewData["Patient"] = _context.patients.ToList();
                ViewData["times"] = _context.times.ToList();
                return View();
            }
            else
            {
                return View();
            }
        }

        public IActionResult DoAppointment(int id)
        {
            Response.Cookies.Append("appointmentId", id.ToString());

            ViewData["Appointment"] = _context.appointments.Where(a => a.Id == Convert.ToInt32(Request.Cookies["appointmentId"])).ToList();
            ViewData["Services"] = _context.services.ToList();
            ViewData["Doctor"] = _context.doctors.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
            ViewData["Patient"] = _context.patients.ToList();
            ViewData["times"] = _context.times.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConclusion([Bind("Complaints,Recommendations,Diagnosis")] Сonclusion сonclusion)
        {
            Appointment appointment = new Appointment();
            appointment.Id = Convert.ToInt32(Request.Cookies["appointmentId"]);
            appointment.Status = 3;
            appointment.DateOfAppointment = _context.appointments.Where(a => a.Id == appointment.Id).Select(a => a.DateOfAppointment).FirstOrDefault();
            appointment.PatientId = _context.appointments.Where(a => a.Id == appointment.Id).Select(a => a.PatientId).FirstOrDefault();
            appointment.DoctorId = _context.appointments.Where(a => a.Id == appointment.Id).Select(a => a.DoctorId).FirstOrDefault();
            appointment.ServiceId = _context.appointments.Where(a => a.Id == appointment.Id).Select(a => a.ServiceId).FirstOrDefault();
            appointment.timeId = _context.appointments.Where(a => a.Id == appointment.Id).Select(a => a.timeId).FirstOrDefault();
            

            
            _context.appointments.Update(appointment);
            await _context.SaveChangesAsync();

            сonclusion.AppointmentId = appointment.Id;

            Response.Cookies.Delete("appointmentId");

            _context.сonclusions.Add(сonclusion);
            await _context.SaveChangesAsync();

            return Redirect("~/Doctor/SuccessCreate");
        }
    }
}
