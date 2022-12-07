using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KurierzyDomain;
using KurierzyDTOs;

namespace KurierzyAPI.Pages.Persons
{
    public class CreateModel : PageModel
    {


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }
        public RegisterPersonDTO RegisterPersonDTO { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            return Redirect("/Index");
        }
    }
}
