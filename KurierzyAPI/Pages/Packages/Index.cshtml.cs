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
    public class IndexModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public IndexModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        public IList<Package> Package { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Packages != null)
            {
                Package = await _context.Packages
                .Include(p => p.Deliverer).ToListAsync();
            }
        }
    }
}
