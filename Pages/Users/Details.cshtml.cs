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
    
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly FinalProject.Models.Context _context;
        

        public DetailsModel(FinalProject.Models.Context context, ILogger<DetailsModel> logger)
        {
            _logger = logger;
            _context = context;
        }

       new public User User { get; set; }
       [BindProperty]
       public int ProductIdToDelete {get;set;}
        [BindProperty]
        [Display(Name = "Product")]
        public int ProductIdToAdd {get; set;}
        public List<Product> AllProducts {get; set;}
        public SelectList ProductDropDown {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).FirstOrDefaultAsync(m => m.userID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
         public async Task<IActionResult> OnPostDeleteProductAsync(int? id)
        {
            _logger.LogWarning($"OnPost: UserId {id}, DELETE product {ProductIdToDelete}");
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).FirstOrDefaultAsync(m => m.userID == id);
            AllProducts = await _context.Product.ToListAsync();
            ProductDropDown = new SelectList(AllProducts, "ProductID", "Description");
            
            if (User == null)
            {
                return NotFound();
            }

            UserProduct productToRemove= _context.UserProduct.Find(ProductIdToDelete, id.Value);

            if (productToRemove != null)
            {
                _context.Remove(productToRemove);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Product NOT in the List");
            }

            return RedirectToPage(new {id = id});
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            _logger.LogWarning($"OnPost: ProductId {id}, ADD product {ProductIdToAdd}");
            if (ProductIdToAdd == 0)
            {
                ModelState.AddModelError("ProductIdToAdd", "This field is a required field.");
                return Page();
            }
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).FirstOrDefaultAsync(m => m.userID == id);            
            AllProducts = await _context.Product.ToListAsync();
            ProductDropDown = new SelectList(AllProducts, "ProductID", "Description");

            if (User == null)
            {
                return NotFound();
            }

            if (!_context.UserProduct.Any(sc => sc.ProductID == ProductIdToAdd && sc.UserID == id.Value))
            {
                UserProduct productToAdd = new UserProduct { UserID = id.Value, ProductID = ProductIdToAdd};
                _context.Add(productToAdd);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Product is already in the list.");
            }

            // Best practice is that OnPost should redirect. This clears the form data.
            // FIXME: Can we just populate the routeValues from what is already there?
            return RedirectToPage(new {id = id});
        }
    }
}
