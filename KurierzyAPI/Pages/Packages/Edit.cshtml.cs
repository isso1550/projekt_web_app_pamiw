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

namespace KurierzyAPI.Pages.Packages
{
    public class EditModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public EditModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Package Package { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Packages == null)
            {
                return NotFound();
            }

            var package =  await _context.Packages.FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }
            Package = package;
           ViewData["DelivererId"] = new SelectList(_context.Deliverers, "PersonId", "PersonId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(Package.Id))
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

        private bool PackageExists(int id)
        {
          return _context.Packages.Any(e => e.Id == id);
        }
    }
}
