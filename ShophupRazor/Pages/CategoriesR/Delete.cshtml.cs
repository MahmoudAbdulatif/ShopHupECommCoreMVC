using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShophupRazor.DataR;
using ShophupRazor.ModelsR;

namespace ShophupRazor.Pages.CategoriesR
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContextR _db;
        public CategoryR CategoryR { get; set; }
        public DeleteModel(AppDbContextR db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if(id != null || id!= 0) {CategoryR = _db.CategoriesR.Find(id); }
        }
        public IActionResult OnPost()
        {
            //_db.CategoriesR.Remove(CategoryR);
            //_db.SaveChanges();
            //return RedirectToPage("index");


            CategoryR? obj = _db.CategoriesR.Find(CategoryR.Id);
            if (obj == null) { return NotFound(); }
            _db.CategoriesR.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Removed Successfully";
            return RedirectToPage("index");
        }
    }
}
