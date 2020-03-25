using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spice.Models;
using Spice.Models.ViewModels;
using Spie.Data;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        [TempData]
        public string StatusMessage { get; set; }


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

        //GET CREATE

        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await dbContext.Category.ToListAsync(),
                SubCategory = new SubCategory(),
                SubCategoryList = await dbContext.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync<String>()

            };
            return View(model);
            
        }

        //POST - CREATE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExsist = dbContext.SubCategory.Include(c => c.Category).Where(s => s.Name == model.SubCategory.Name && s.Category.Id == model.SubCategory.CategoryId);
                if (doesSubCategoryExsist.Count() > 0)
                {
                    //Error
                    StatusMessage = "Error: Sub Category exists under " + doesSubCategoryExsist.First().Category.Name + " category. Please use another name ";
                }
                else
                {
                    dbContext.SubCategory.Add(model.SubCategory);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategoryAndCategoryViewModel viewModel = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await dbContext.Category.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await dbContext.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(viewModel);

        }

        [ActionName("GetSubCategory")]
        public async Task<IActionResult>GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            subCategories = await(from subCategory in dbContext.SubCategory where subCategory.CategoryId == id
                             select subCategory).ToListAsync();
            return Json(new SelectList(subCategories, "Id", "Name"));

        }

        //GET EDIT
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var subCat = await dbContext.SubCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (subCat == null)
            {
                return NotFound();
            }

            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await dbContext.Category.ToListAsync(),
                SubCategory = subCat,
                SubCategoryList = await dbContext.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync<String>()

            };
            return View(model);

        }

        //POST - EDIT

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,SubCategoryAndCategoryViewModel model)
        {
            if (id == null)
            {

            }

            if (ModelState.IsValid)
            {
                var doesSubCategoryExsist = dbContext.SubCategory.Include(c => c.Category).Where(s => s.Name == model.SubCategory.Name && s.Category.Id == model.SubCategory.CategoryId);
                if (doesSubCategoryExsist.Count() > 0)
                {
                    //Error
                    StatusMessage = "Error: Sub Category exists under " + doesSubCategoryExsist.First().Category.Name + " category. Please use another name ";
                }
                else
                {
                    dbContext.SubCategory.Add(model.SubCategory);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategoryAndCategoryViewModel viewModel = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await dbContext.Category.ToListAsync(),
                SubCategory = model.SubCategory,
                SubCategoryList = await dbContext.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(viewModel);

        }


    }
}