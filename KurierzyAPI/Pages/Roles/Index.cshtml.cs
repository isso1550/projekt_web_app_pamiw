using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KurierzyDB;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly KurierzyDBContext _context;

        public IndexModel(KurierzyDBContext context)
        {
            _context = context;
        }

        public IList<Role> Role { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Roles != null)
            {
                Role = await _context.Roles.ToListAsync();
            }
        }
    }
}
