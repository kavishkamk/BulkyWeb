using BulkyWebRazer_Temp.Data;
using BulkyWebRazer_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazer_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; } 

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? CategoryId)
        {
            if (CategoryId != null && CategoryId > 0)
            {
				Category = _db.Categories.Find(CategoryId);
			}
        }

        public IActionResult OnPost() {

            if (ModelState.IsValid)
            {
				_db.Categories.Update(Category);
				_db.SaveChanges();
				return RedirectToPage("Index");
			}
            return Page();
           
        }
    }
}
