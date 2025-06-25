using EComm.DataAccess.Data;
using EComm.DataAccess.Repository.IRepository;
using EComm.Models;
using EComm.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopHupECommCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(CategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if(category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder CanNot exactly match the Name");
            //}
            
            if (ModelState.IsValid)
            {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var category = _unitOfWork.Category.Get(e=>e.Id==id);
            if (category == null) { return NotFound(); }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            
            if (ModelState.IsValid)
            {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var category =_unitOfWork.Category.Get(e=>e.Id == id);
            if (category == null) { return NotFound(); }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            Category? category = _unitOfWork.Category.Get(e => e.Id == id);
            if(category == null) { return NotFound(); }
            _unitOfWork.Category.Remove(category);
           _unitOfWork.Save();
            TempData["success"] = "Category Removed Successfully";
            return RedirectToAction("Index");
        }
    } 
}
