using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShophupRazor.DataR;
using ShophupRazor.ModelsR;

namespace ShophupRazor.Pages.CategoriesR
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContextR _db;
        public List<CategoryR> ListCategoryR { get; set; }
        public IndexModel(AppDbContextR db)
        {
            _db = db;
        }
        public void OnGet()
        {
            ListCategoryR = _db.CategoriesR.ToList();
        }
    }
}
