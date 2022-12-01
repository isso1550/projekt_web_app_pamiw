using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KurierzyDB;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Packages
{
    public class CreateModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public CreateModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DelivererId"] = new SelectList(_context.Deliverers, "PersonId", "PersonId");
            return Page();
        }

        [BindProperty]
        public Package Package { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Packages.Add(Package);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
