using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergySolution.Data;
using AgriEnergySolution.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AgriEnergySolution.Controllers
{

    [Authorize(Roles = "Employee")]

    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public EmployeeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmer.ToListAsync());
        }

        public async Task<IActionResult> Filter(string searchString, string startDate = "", string endDate = "", string category = "")
        {
            var products = from p in _context.Products
                           select p;

            var farmer =  _context.Farmer;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString) || s.ProductCategory.Contains(searchString));
            }


            // Convert date strings to DateTime objects
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MaxValue;

            if (!string.IsNullOrEmpty(startDate))
            {
                start = DateTime.Parse(startDate);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                end = DateTime.Parse(endDate);
            }

            // Apply filters
              products = 
             
             products.Where(p => p.ProductionDate >= start && p.ProductionDate <= end);
             

           

            return View(await products.ToListAsync());

        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,Name")] Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmer);
                await _context.SaveChangesAsync();

                // Create the user and assign the role
                var user = new IdentityUser
                {
                    UserName = farmer.Email,
                    Email = farmer.Email

                };

                var result = await _userManager.CreateAsync(user, farmer.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Farmer");
                }
                else
                {
                    // Handle the case where user creation failed
                    // You might want to log the error and display a message to the user
                }

                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }

        // GET: Employee/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmer.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }
            return View(farmer);
        }

      

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,Name")] Farmer farmer)
        {
            if (id != farmer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }
        // GET: Employee/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            var farmer = await _context.Farmer.FindAsync(id);
            if (farmer != null)
            {
                // Find the user associated with the farmer
                var user = await _userManager.FindByEmailAsync(farmer.Email);
                if (user != null)
                {
                    // Remove the user from the AspNetUserRoles table
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }

                    // Delete the user from the AspNetUsers table
                    await _userManager.DeleteAsync(user);
                }

                _context.Farmer.Remove(farmer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    var farmer = await _context.Farmer
                        .Include(f => f.FarmerProducts)
                        .ThenInclude(fp => fp.Products)
                        .FirstOrDefaultAsync(f => f.Id == id);

                    if (farmer != null)
                    {
                        // Remove related FarmerProducts entries and the associated Products
                        foreach (var farmerProduct in farmer.FarmerProducts.ToList())
                        {
                            // Remove the associated Product
                            var product = await _context.Products.FindAsync(farmerProduct.ProductId);
                            if (product != null)
                            {
                                _context.Products.Remove(product);
                            }

                            // Remove the FarmerProducts entry
                            _context.FarmerProducts.Remove(farmerProduct);
                        }

                        // Remove the Farmer
                        _context.Farmer.Remove(farmer);

                        // Save changes and commit the transaction
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        // Handle case where farmer not found
                        ModelState.AddModelError(string.Empty, "Farmer not found.");
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception details
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> FarmerProducts(int? id, string startDate = "", string endDate = "", string category = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmer
              .FirstOrDefaultAsync(m => m.Id == id);
            if (farmer == null)
            {
                return NotFound();
            }

            // Convert date strings to DateTime objects
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MaxValue;

            if (!string.IsNullOrEmpty(startDate))
            {
                start = DateTime.Parse(startDate);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                end = DateTime.Parse(endDate);
            }

            // Apply filters
            var filteredProducts = await _context.Products
             .Where(p => _context.FarmerProducts.Any(fp => fp.FarmerId == farmer.Id && fp.ProductId == p.ProductId))
             .Where(p => !string.IsNullOrEmpty(category) ? p.ProductCategory == category : true)
             .Where(p => p.ProductionDate >= start && p.ProductionDate <= end)
             .ToListAsync();

            return View(filteredProducts);



        }



        public IActionResult ViewFarmerProducts()
        {
            return RedirectToAction(nameof(FarmerProducts));


        }






        private bool FarmerExists(int id)
        {
            return _context.Farmer.Any(e => e.Id == id);
        }
    }
}