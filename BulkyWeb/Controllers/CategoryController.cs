using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
           _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {

            List<Category> categories = _categoryRepository.GetAll();

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
                _categoryRepository.Add(category);
                _categoryRepository.Save();
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

            //Category? categoryFromDb = _db.Categories.Find(categoryId);
            Category? categoryFromDb = _categoryRepository.Get(u => u.CategoryId == categoryId);
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
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
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

            Category? categoryFromDB = _categoryRepository.Get(u => u.CategoryId == categoryId);

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

            Category? categoryFromDb = _categoryRepository.Get(u => u.CategoryId == categoryId);

			if (categoryFromDb == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(categoryFromDb);
            _categoryRepository.Save();
			TempData["success"] = "Category Deleted Successfully";

			return RedirectToAction("Index");
        }
    }
}
