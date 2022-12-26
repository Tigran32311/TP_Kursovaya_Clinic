using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;

namespace Clinic.Controllers
{
    public class AdminController : Controller
    {
        private readonly ClinicDbContext _context;

        public AdminController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult MainPageAdmin()
        {
            return View();
        }

        // GET: Admin
        public async Task<IActionResult> IndexDoctors()
        {
            var clinicDbContext = _context.doctors.Include(d => d.Specialization);
            return View(await clinicDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexServices()
        {
            var clinicDbContext = _context.services.Include(d => d.Specialization);
            return View(await clinicDbContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> DetailsDoctors(int? id)
        {
            if (id == null || _context.doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.doctors
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        public async Task<IActionResult> DetailsServices(int? id)
        {
            if (id == null || _context.services == null)
            {
                return NotFound();
            }

            var doctor = await _context.services
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Admin/Create
        public IActionResult CreateDoctors()
        {
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id");
            return View();
        }

        public IActionResult CreateServices()
        {
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDoctors([Bind("Id,Name,Surname,Fathername,Email,Password,WorkShift,SpecializationId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id", doctor.SpecializationId);
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServices([Bind("Id,ServiceName,SpecializationId")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexServices));
            }
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id", service.SpecializationId);
            return View(service);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> EditServices(int? id)
        {
            if (id == null || _context.services == null)
            {
                return NotFound();
            }

            var service = await _context.services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id", service.SpecializationId);
            return View(service);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDoctors(int id, [Bind("Id,Name,Surname,Fathername,Email,Password,WorkShift,SpecializationId")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id", doctor.SpecializationId);
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditServices(int id, [Bind("Id,ServiceName,SpecializationId")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecializationId"] = new SelectList(_context.specializations, "Id", "Id", service.SpecializationId);
            return View(service);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> DeleteDoctors(int? id)
        {
            if (id == null || _context.doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.doctors
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        public async Task<IActionResult> DeleteServices(int? id)
        {
            if (id == null || _context.services == null)
            {
                return NotFound();
            }

            var service = await _context.services
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedDoctors(int id)
        {
            if (_context.doctors == null)
            {
                return Problem("Entity set 'ClinicDbContext.doctors'  is null.");
            }
            var doctor = await _context.doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.doctors.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return _context.doctors.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedServices(int id)
        {
            if (_context.doctors == null)
            {
                return Problem("Entity set 'ClinicDbContext.doctors'  is null.");
            }
            var service = await _context.services.FindAsync(id);
            if (service != null)
            {
                _context.services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.services.Any(e => e.Id == id);
        }
    }
}
