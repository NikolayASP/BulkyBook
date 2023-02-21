using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable <Category> categoryList= _context.Categories;
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            }

            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Category name already in use.");
            }

            if (_context.Categories.Any(c => c.DisplayOrder == category.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "Display order already in use");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            }

            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Category name already in use.");
            }

            if (_context.Categories.Any(c => c.DisplayOrder == category.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "Display order already in use");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
