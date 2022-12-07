using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Persons
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Person Person { get; set; } = default!;


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPut()
        {
            return RedirectToPage("./Index");
        }
    }
}
