using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
                Category = _db.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            var category = _db.Categories.FirstOrDefault(o => o.Id == Category.Id);

            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted sucessfully";
            return RedirectToPage(nameof(Index));

        }
    }
}
