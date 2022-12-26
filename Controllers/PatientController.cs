using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Clinic.Controllers
{
    public class PatientController : Controller
    {
        private readonly ClinicDbContext _context;
        public PatientController(ClinicDbContext context)
        {
            _context = context;
        }


        public IActionResult MainPagePatient() //добавить данные клиента
        {
            ViewData["Patient"] = _context.patients.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
            return View();
        }

        public IActionResult SuccessCreate()
        {
            return View();
        }

        public IActionResult MakeAppointmentChooseService()
        {
            ViewData["Specialization"] = _context.specializations.ToList();
            ViewData["Services"] = _context.services.ToList();
            return View();
        }

        public IActionResult MakeAppointmentChooseDoctor(int id)
        {
            //получение специализации услуги выбранной пользователем
            int specId = _context.services.Where(a=> a.Id==id).Select(a=> a.SpecializationId).FirstOrDefault();

            Response.Cookies.Append("servicesId", id.ToString());
            Response.Cookies.Append("specId", specId.ToString());

            //отправка данных на представление
            ViewData["Specialization"] = _context.specializations.ToList();
            ViewData["Doctors"] = _context.doctors.Where(a=> a.SpecializationId==specId).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult MakeAppointmentChooseData(int id)
        {
            Response.Cookies.Append("docId", id.ToString());

            // логика расписания int docId
            ViewData["Services"] = _context.services.Where(a => a.Id == Convert.ToInt32(Request.Cookies["servicesId"])).ToList();
            ViewData["Specialization"] = _context.specializations.Where(a => a.Id == Convert.ToInt32(Request.Cookies["specId"])).ToList();
            ViewData["Doctors"] = _context.doctors.Where(a => a.Id == id).ToList();
            ViewData["Appointment"] = _context.appointments.ToList();

            return View();
        }

        [HttpPost,ActionName("MakeAppointmentChooseData")]
        public  async Task<IActionResult> MakeAppointmentChooseData(DateTime DateOfAppointment)
        {
            Appointment appointment = new Appointment();

            Response.Cookies.Append("dateOfApp", DateOfAppointment.ToString());


            ViewData["times"] = await _context.times.ToListAsync();

            ViewData["Services"] = _context.services.Where(a => a.Id == Convert.ToInt32(Request.Cookies["servicesId"])).ToList();
            ViewData["Specialization"] = _context.specializations.Where(a => a.Id == Convert.ToInt32(Request.Cookies["specId"])).ToList();
            ViewData["Doctors"] = _context.doctors.Where(a => a.Id == Convert.ToInt32(Request.Cookies["docId"])).ToList();
            if (DateOfAppointment > DateTime.Now)
            {
                ViewData["Appointment"] = _context.appointments.Where(a => a.DateOfAppointment == DateOfAppointment).ToList();
            }

            return View(Convert.ToInt32(Request.Cookies["docId"]));
        }


        public async Task<IActionResult> CreateApp(int id)
        {

            Appointment appointment = new Appointment();
            appointment.DateOfAppointment = Convert.ToDateTime(Request.Cookies["dateOfApp"]);
            appointment.Status = 1;
            appointment.PatientId = Convert.ToInt32(Request.Cookies["userId"]);
            appointment.DoctorId = Convert.ToInt32(Request.Cookies["docId"]);
            appointment.ServiceId = Convert.ToInt32(Request.Cookies["servicesId"]);
            appointment.timeId = id;

            Response.Cookies.Delete("docId");
            Response.Cookies.Delete("servicesId");
            Response.Cookies.Delete("dateOfApp");

            _context.appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Redirect("~/Patient/SuccessCreate");

        }

        public IActionResult LookAppointments()
        {
            ViewData["Appointments"] = _context.appointments
                .Where(a => a.PatientId == Convert.ToInt32(Request.Cookies["userId"]) && a.DateOfAppointment >= DateTime.Now && a.Status<3)
                .ToList();
            ViewData["Services"] = _context.services.ToList();
            ViewData["Specialization"] = _context.specializations.ToList();
            ViewData["Doctors"] = _context.doctors.ToList();
            ViewData["times"] = _context.times.ToList();
            return View();
        }
        public IActionResult LookAmbulancyCard()
        {
            ViewData["Appointments"] = _context.appointments.ToList();
            ViewData["Services"] = _context.services.ToList();
            ViewData["Specialization"] = _context.specializations.ToList();
            ViewData["Doctors"] = _context.doctors.ToList();
            ViewData["times"] = _context.times.ToList();

            ViewData["Conclusions"] = _context.сonclusions
                .Where(a => a.AppointmentId == _context.appointments.Where(a => a.PatientId == Convert.ToInt32(Request.Cookies["userId"]))
                .Select(a=>a.Id).FirstOrDefault())
                .ToList();
            return View();
        }

        public async Task<IActionResult> DeleteAppointments(int id)
        {

            var appointment = await _context.appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();


            ViewData["Appointments"] = _context.appointments
                .Where(a => a.PatientId == Convert.ToInt32(Request.Cookies["userId"]) && a.DateOfAppointment >= DateTime.Now && a.Status < 3)
                .ToList();
            ViewData["Services"] = _context.services.ToList();
            ViewData["Specialization"] = _context.specializations.ToList();
            ViewData["Doctors"] = _context.doctors.ToList();
            ViewData["times"] = _context.times.ToList();


            return Redirect("~/Patient/LookAppointments");
        }

        public IActionResult ListServices()
        {
            ViewData["Services"] = _context.services.Include(s => s.Specialization).ToList();
            return View();
        }
    }
}
