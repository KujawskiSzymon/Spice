using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spie.Data;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubCategoryController(ApplicationDbContext db)
        {
            dbContext = db;
        }

        //GET INDEX
        public async Task<IActionResult> Index()
        {
            var SubCategories = await dbContext.SubCategory.Include(cat=>cat.Category).ToListAsync();
            return View(SubCategories);
        }

       
    }
}