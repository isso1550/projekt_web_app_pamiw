using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KurierzyDB;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Deliverers
{
    public class DeleteModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public DeleteModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Deliverer Deliverer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Deliverers == null)
            {
                return NotFound();
            }

            var deliverer = await _context.Deliverers.FirstOrDefaultAsync(m => m.PersonId == id);

            if (deliverer == null)
            {
                return NotFound();
            }
            else 
            {
                Deliverer = deliverer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Deliverers == null)
            {
                return NotFound();
            }
            var deliverer = await _context.Deliverers.FindAsync(id);

            if (deliverer != null)
            {
                Deliverer = deliverer;
                _context.Deliverers.Remove(Deliverer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
