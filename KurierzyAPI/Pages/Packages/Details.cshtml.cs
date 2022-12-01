using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KurierzyDB;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Packages
{
    public class DetailsModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public DetailsModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

      public Package Package { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Packages == null)
            {
                return NotFound();
            }

            var package = await _context.Packages.FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }
            else 
            {
                Package = package;
            }
            return Page();
        }
    }
}
