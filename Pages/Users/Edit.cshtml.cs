using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.Extensions.Logging;

namespace FinalProject.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly FinalProject.Models.Context _context;
        private readonly ILogger _logger;

        public EditModel(FinalProject.Models.Context context,ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        new public User User { get; set; }
        public List<Product> Products {get;set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).FirstOrDefaultAsync(m => m.userID == id);
            
            Products = _context.Product.ToList();

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] selectedProducts)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userToUpdate = await _context.User.Include(s => s.UserProducts).ThenInclude(sc => sc.Product).FirstOrDefaultAsync(m => m.userID == User.userID);
           userToUpdate.Name = User.Name;
           
            UpdateUserProducts(selectedProducts, userToUpdate);


            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.userID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.userID == id);
        }

         private void UpdateUserProducts(int[] selectedProducts, User userToUpdate)
        {
            if (selectedProducts == null)
            {
                userToUpdate.UserProducts = new List<UserProduct>();
                return;
            }

            List<int> currentProducts = userToUpdate.UserProducts.Select(c => c.ProductID).ToList();
            List<int> selectedProductsList = selectedProducts.ToList();

            foreach (var product in _context.Product)
            {
                if (selectedProductsList.Contains(product.ProductID))
                {
                    if (!currentProducts.Contains(product.ProductID))
                    {
                        // Add course here
                        userToUpdate.UserProducts.Add(
                            new UserProduct { userID = userToUpdate.userID, ProductID = product.ProductID }
                        );
                        _logger.LogWarning($"User {userToUpdate.Name}  ({userToUpdate.userID}) - ADD {product.ProductID} {product.Description}");
                    }
                }
                else
                {
                    if (currentProducts.Contains(product.ProductID))
                    {
                        // Remove course here
                        UserProduct productToRemove = userToUpdate.UserProducts.SingleOrDefault(s => s.ProductID == product.ProductID);
                        _context.Remove(productToRemove);
                        _logger.LogWarning($"User {userToUpdate.Name}  ({userToUpdate.userID}) - Delete {product.ProductID} {product.Description}");
                      
    
           
                     }
                 }
            }
    }

}
}
