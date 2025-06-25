using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShophupRazor.DataR;
using ShophupRazor.ModelsR;

namespace ShophupRazor.Pages.CategoriesR
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly AppDbContextR _db;

        public CategoryR CategoryR { get; set; }
        public EditModel(AppDbContextR db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if(id != null || id != 0) { CategoryR = _db.CategoriesR.Find(id); }
        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                _db.CategoriesR.Update(CategoryR);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
