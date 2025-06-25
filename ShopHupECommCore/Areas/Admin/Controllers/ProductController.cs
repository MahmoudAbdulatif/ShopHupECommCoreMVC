using EComm.DataAccess.Data;
using EComm.DataAccess.Repository;
using EComm.DataAccess.Repository.IRepository;
using EComm.Models;
using EComm.Models.ViewModel;
using EComm.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace ShopHupECommCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;  // for images
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var ProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(ProductList);
        }
        //public IActionResult Create()
        //{
        //    //Projections in EF
        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem
        //    {
        //        Text = e.Name,
        //        Value = e.Id.ToString()
        //    });

        //    //ViewBag.CategoryList = CategoryList;
        //    ProductVM productVM = new()
        //    {
        //        CategoryList = CategoryList,
        //        Product = new Product()
        //    };
        //    return View(productVM);
        //}
        
        //[HttpPost]
        //public IActionResult Create(ProductVM productVM)
        //{           
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Add(productVM.Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product Created Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        productVM.CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem
        //        {
        //            Text = e.Name,
        //            Value = e.Id.ToString()
        //        });
        //        return View(productVM);
        //    }
        //}
        
        public IActionResult UpSert(int? id)
        {
            //Projections in EF
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            });

            //ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(e => e.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult UpSert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.Trim('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }


                    using(var filesream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(filesream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\"+filename;
                }
                if(productVM.Product.Id==0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                });
                return View(productVM);
            }
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0) { return NotFound(); }
        //    var product = _unitOfWork.Product.Get(e=>e.Id==id);
        //    if (product == null) { return NotFound(); }
        //    return View(product);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //    _unitOfWork.Product.Update(product);
        //    _unitOfWork.Save();
        //        TempData["success"] = "Product Updated Successfully";
        //    }
        //    return RedirectToAction("Index");
        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0) { return NotFound(); }
        //    var product =_unitOfWork.Product.Get(e=>e.Id == id);
        //    if (product == null) { return NotFound(); }
        //    return View(product);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult ConfirmDelete(int? id)
        //{
        //    Product? product = _unitOfWork.Product.Get(e => e.Id == id);
        //    if(product == null) { return NotFound(); }
        //    _unitOfWork.Product.Remove(product);
        //   _unitOfWork.Save();
        //    TempData["success"] = "Product Removed Successfully";
        //    return RedirectToAction("Index");
        //}






        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = ProductList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.Get(e => e.Id == id);
            if(obj == null)
            {
                return Json(new { success = false, message = "error whiledeleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                 obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }


            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    }
        #endregion

    
}
