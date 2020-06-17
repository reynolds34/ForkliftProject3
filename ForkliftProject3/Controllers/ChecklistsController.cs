using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForkliftProject3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ForkliftProject3
{
    [Authorize]
    public class ChecklistsController : Controller
    {
        private readonly ForkliftContext _context;
        private UserManager<IdentityUser> _userManager;


        public ChecklistsController(ForkliftContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [Authorize]
        public IActionResult NewestChecklist()
        {
            string userName = _userManager.GetUserName(User);
            Checklists profile = _context.Checklists.LastOrDefault(p => p.CompletedBy == userName);

            if (profile == null)
            {
                return RedirectToAction("Create");
            }

            return View(profile);

        }
        //GET: Browse Checklists
        public async Task<IActionResult> Browse()
        {
            return View(await _context.Checklists.ToListAsync());
        }


        // GET: Checklists
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Checklists.ToListAsync());
        }

        // GET: Checklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklists = await _context.Checklists
                .FirstOrDefaultAsync(m => m.ChecklistId == id);
            if (checklists == null)
            {
                return NotFound();
            }

            return View(checklists);
        }

        // GET: Checklists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ChecklistId,CompletedBy,TodaysDate,VinNumber,Battery,Oil,Lights,Horn,Levers,Tires,Forks,Comments")] Checklists checklists)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(checklists);
                    
                    return RedirectToAction("Browse");
                }           
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(checklists);
        }

        // GET: Checklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklists = await _context.Checklists.FindAsync(id);
            if (checklists == null)
            {
                return NotFound();
            }
            return View(checklists);
        }

        // POST: Checklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChecklistId,CompletedBy,TodaysDate,VinNumber,Battery,Oil,Lights,Horn,Levers,Tires,Forks,Comments")] Checklists checklists)
        {
            if (id != checklists.ChecklistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistsExists(checklists.ChecklistId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Browse");
            }
            return View(checklists);
        }


        // GET: Checklists/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklists = await _context.Checklists
                .FirstOrDefaultAsync(m => m.ChecklistId == id);
            if (checklists == null)
            {
                return NotFound();
            }

            return View(checklists);
        }

        // POST: Checklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklists = await _context.Checklists.FindAsync(id);
            _context.Checklists.Remove(checklists);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistsExists(int id)
        {
            return _context.Checklists.Any(e => e.ChecklistId == id);
        }
    }
}
