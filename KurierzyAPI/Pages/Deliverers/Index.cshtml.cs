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
    public class IndexModel : PageModel
    {
        private readonly KurierzyDB.KurierzyDBContext _context;

        public IndexModel(KurierzyDB.KurierzyDBContext context)
        {
            _context = context;
        }

        public IList<Deliverer> Deliverer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Deliverers != null)
            {
                Deliverer = await _context.Deliverers
                .Include(d => d.Person).Include(d=> d.Packages).ToListAsync();

               // Deliverer d = _context.Deliverers.FirstOrDefault(p => p.PersonId == 2);
                //List<Package> p = _context.Deliverers.Include(x => x.Packages).FirstOrDefault(p => p.PersonId == 2).Packages;
                //Console.WriteLine(String.Join(", " , p.ToArray().ToString()));
            }
        }
    }
}
