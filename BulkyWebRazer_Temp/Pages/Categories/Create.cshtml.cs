using BulkyWebRazer_Temp.Data;
using BulkyWebRazer_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazer_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
		private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

		public void OnGet()
        {
        }

        public IActionResult OnPost(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
			TempData["success"] = "Category Created Successfully";
			return RedirectToPage("Index");
        }
    }
}
