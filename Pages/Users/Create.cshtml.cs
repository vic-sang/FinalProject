using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly FinalProject.Models.Context _context;
        private readonly ILogger<CreateModel> _logger;
        public List<Product> Products {get;set;}
        public List<User> Users {get;set;}
         
      
       

        public CreateModel(FinalProject.Models.Context context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
            [BindProperty]
        new public User User { get; set; }
        public Product Product {get; set;}

       
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(User);
           
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
