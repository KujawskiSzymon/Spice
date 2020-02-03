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
    public class CategoryController : Controller
    {
       private readonly ApplicationDbContext dbContext; 

        public CategoryController(ApplicationDbContext db)
        {
            dbContext = db;
        }
        public async Task< IActionResult> Index()
        {

            return  View(await dbContext.Category.ToListAsync());
        }
        //GET for Create
        public IActionResult Create()
        {
            return View();
        }
    }
}