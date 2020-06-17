using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForkliftProject3.Models;
using Microsoft.AspNetCore.Authorization;

namespace ForkliftProject3.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly ForkliftContext _context;

        public VehiclesController(ForkliftContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrator")]
        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles.ToListAsync());
        }


        //GET: Browse Checklists
        public async Task<IActionResult> Browse()
        {
            return View(await _context.Vehicles.ToListAsync());
        }





        // GET: Vehicles/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Vin,MakeName,Active,Year")] Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicles);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles == null)
            {
                return NotFound();
            }
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Vin,MakeName,Active,Year")] Vehicles vehicles)
        {
            if (id != vehicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclesExists(vehicles.Id))
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
            return View(vehicles);
        }

        // GET: Vehicles/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclesExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
