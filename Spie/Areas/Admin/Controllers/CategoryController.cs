﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Models;
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
        public async Task<IActionResult> Index()
        {

            return View(await dbContext.Category.ToListAsync());
        }
        //GET for Create
        public IActionResult Create()
        {
            return View();
        }
        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Category.Add(category);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // GET - EDIT

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var category = await dbContext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Update(category);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var category = await dbContext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST _ DELETE

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                var cat = await dbContext.Category.FindAsync(id);
                if (cat == null)
                    return NotFound();
                dbContext.Category.Remove(cat);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return NotFound();
        }

        //Assigment 1:
        //Get Details
        public async Task<IActionResult>Details(int? id)
        {
            if (ModelState.IsValid)
            {
                var cat = await dbContext.Category.FindAsync(id);
                return View(cat);
            }
            return NotFound();
        }


    }
}