using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KurierzyDB;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Deliverers
{
    public class EditModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public EditModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deliverer Deliverer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Deliverers == null)
            {
                return NotFound();
            }

            var deliverer =  await _context.Deliverers.FirstOrDefaultAsync(m => m.PersonId == id);
            if (deliverer == null)
            {
                return NotFound();
            }
            Deliverer = deliverer;
           ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
 
            _context.Attach(Deliverer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelivererExists(Deliverer.PersonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DelivererExists(int id)
        {
          return _context.Deliverers.Any(e => e.PersonId == id);
        }
    }
}
