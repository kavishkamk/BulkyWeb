using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public IActionResult Index()
		{
			List<Product> products = _productRepository.GetAll();
			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			if (ModelState.IsValid)
			{
				_productRepository.Add(product);
				_productRepository.save();
				TempData["success"] = "Product Updated Successfully";
				return RedirectToAction("Index");
			}

			return View();
			
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id < 1)
			{
				return NotFound();
			}

			Product product = _productRepository.Get(u => u.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_productRepository.update(product);
				_productRepository.save();
				TempData["success"] = "Product Updated Successfully";
				return RedirectToAction("Index");
			}

			return View() ;
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id < 1)
			{
				return NotFound();
			}

			Product product = _productRepository.Get(u => u.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}


		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null || id < 1)
			{
				return NotFound();
			}

			Product product = _productRepository.Get(u => u.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			_productRepository.Delete(product);
			_productRepository.save();
			TempData["success"] = "Product Deleted Successfully";

			return RedirectToAction("Index");
		}
	}
}
