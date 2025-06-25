using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShophupRazor.DataR;
using ShophupRazor.ModelsR;

namespace ShophupRazor.Pages.CategoriesR
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContextR _db;
        [BindProperty]
        public CategoryR CategoryR { get; set; }
        public CreateModel(AppDbContextR db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            _db.CategoriesR.Add(CategoryR);
            _db.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("index");
        }
    }
}
