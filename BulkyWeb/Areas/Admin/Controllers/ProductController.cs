using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly ICategoryRepository _categoryRepository;

		public ProductController(IProductRepository productRepository,
			ICategoryRepository categoryRepository,
			IWebHostEnvironment webHostEnvironment)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			List<Product> products = _productRepository.GetAll();
			return View(products);
		}

		public IActionResult Upsert(int? id)
		{
			IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.CategoryId.ToString()
			});

			//ViewBag.CategoryList = categoryList;
			//ViewData["CategoryList"] = categoryList;

			ProductVM productVM = new ProductVM()
			{
				CategoryList = categoryList,
				Product = new Product()
			};

			if (id == null || id == 0)
			{
				return View(productVM);
			} else
			{
				productVM.Product = _productRepository.Get(u => u.Id == id);
				return View(productVM);
			}
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if(file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if(!string.IsNullOrEmpty(productVM.Product.ImageUrl)) { 
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					productVM.Product.ImageUrl = @"\images\product\" + fileName;
				}

				if (productVM.Product.Id == 0)
				{
					_productRepository.Add(productVM.Product);
				} else
				{
					_productRepository.update(productVM.Product);
				}

				
				_productRepository.save();
				TempData["success"] = "Product Updated Successfully";
				return RedirectToAction("Index");
			}
			else
			{
				productVM.CategoryList = _categoryRepository.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.CategoryId.ToString()
				});
				return View(productVM);
			}
			
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
