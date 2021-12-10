using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [BindProperty(SupportsGet = true)]
        public int PageNum {get; set;} = 1;
        public int PageSize {get; set;} = 10;
        [BindProperty (SupportsGet = true)]
        public string CurrentSort {get;set;}
        public SelectList SortList {get;set;}

        [BindProperty(SupportsGet = true)]
        public string SearchString {get; set;}
        public string CurrentFilter {get;set;}
        public string NameSort { get; set; }

       

        public async Task OnGetAsync(string CurrentShort, string searchString)
        {
             NameSort = String.IsNullOrEmpty(CurrentShort) ? "name_desc" : "";
             CurrentFilter = searchString;

            IQueryable<User> users = from s in _context.User
                    select s;

            if (!string.IsNullOrEmpty(searchString))
                {
                users = users.Where(s => s.Name.Contains(searchString));
                 }

               


            
            var query = _context.User.Select(s =>s);
            List<SelectListItem> sortItems = new List<SelectListItem> {
                new SelectListItem {Text = "Name Ascending", Value = "first_asc"},
                new SelectListItem {Text = "Name Descending", Value = "first_desc"}
            };
            SortList = new SelectList(sortItems, "Value", "Text", CurrentSort);


            switch (CurrentSort)
            {
                case "first_asc":
                    query = query.OrderBy (s => s.Name);
                    break;
                    case "first_desc":
                        query = query.OrderByDescending(s => s.Name);
                        break;
            }
             User = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).ToListAsync();
            User = await query.Skip((PageNum-1)*PageSize).Take(PageSize).ToListAsync();
           // User = await _context.User.Skip((PageNum-1)*PageSize).Take(PageSize).ToListAsync();
            
        }
    }
}
