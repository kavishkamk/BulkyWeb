using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            List<Category> categories = _db.Categories.ToList<Category>();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Name and Display order should not be same");
            //}

            if (ModelState.IsValid)
            {
				_db.Categories.Add(category);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

            return View();
           
        }
    }
}
