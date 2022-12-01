using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KurierzyDB;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Deliverers
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
        ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Deliverer Deliverer { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Deliverers.Add(Deliverer);
            await _context.SaveChangesAsync();

          

            return RedirectToPage("./Index");
        }
    }
}
