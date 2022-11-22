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

namespace KurierzyWebApp.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public EditModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person =  await _context.Persons.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Person = person;
           ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (!ModelState.IsValid)
            {
                return Page();
            }
            */
            _context.Attach(Person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(Person.Id))
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

        private bool PersonExists(int id)
        {
          return _context.Persons.Any(e => e.Id == id);
        }
    }
}
