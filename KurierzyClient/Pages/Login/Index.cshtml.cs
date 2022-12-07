using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KurierzyDTOs;
using KurierzyDomain;

namespace KurierzyAPI.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginPersonDTO LoginPersonDTO { get; set; }
        
    }
}
