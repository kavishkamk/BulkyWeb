using Bulky.DataAccess.Data;
using Bulky.Models;
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
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display order should not be same");
            }

            if (ModelState.IsValid)
            {
				_db.Categories.Add(category);
				_db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
				return RedirectToAction("Index");
			}

            return View();
           
        }

        public IActionResult Edit(int? categoryId)
        {

            if (categoryId == null || categoryId < 1)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(categoryId);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u => u.CategoryId == categoryId);
            //Category? categoryFromDB2 = _db.Categories.Where(u => u.CategoryId == categoryId).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
            }

            return View() ;
        }

        public IActionResult Delete(int? categoryId)
        {
            if (categoryId == null || categoryId < 0)
            {
                return NotFound();
            }

            Category? categoryFromDB = _db.Categories.Find(categoryId);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? categoryId)
        {
            if (categoryId == null || categoryId < 1)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(categoryId);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";

			return RedirectToAction("Index");
        }
    }
}
