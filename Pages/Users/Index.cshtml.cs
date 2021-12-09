using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly FinalProject.Models.Context _context;

        public IndexModel(FinalProject.Models.Context context)
        {
            _context = context;
        }

        new public IList<User> User { get;set; }
        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).ToListAsync();
        }
    }
}
