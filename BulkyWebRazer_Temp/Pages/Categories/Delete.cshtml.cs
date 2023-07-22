using BulkyWebRazer_Temp.Data;
using BulkyWebRazer_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazer_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category? category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? CategoryId)
        {
            if (CategoryId != null && CategoryId > 0)
            {
				category = _db.Categories.Find(CategoryId);
			}
        }

        public IActionResult OnPost(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
