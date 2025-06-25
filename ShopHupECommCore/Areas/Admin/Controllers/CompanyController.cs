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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var CompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(CompanyList);
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
        //    CompanyVM CompanyVM = new()
        //    {
        //        CategoryList = CategoryList,
        //        Company = new Company()
        //    };
        //    return View(CompanyVM);
        //}
        
        //[HttpPost]
        //public IActionResult Create(CompanyVM CompanyVM)
        //{           
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Add(CompanyVM.Company);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Company Created Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        CompanyVM.CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem
        //        {
        //            Text = e.Name,
        //            Value = e.Id.ToString()
        //        });
        //        return View(CompanyVM);
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
           
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //Update
                Company companyObj = _unitOfWork.Company.Get(e => e.Id == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        public IActionResult UpSert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
                
                if(CompanyObj.Id==0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(CompanyObj);
            }
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0) { return NotFound(); }
        //    var Company = _unitOfWork.Company.Get(e=>e.Id==id);
        //    if (Company == null) { return NotFound(); }
        //    return View(Company);
        //}
        //[HttpPost]
        //public IActionResult Edit(Company Company)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //    _unitOfWork.Company.Update(Company);
        //    _unitOfWork.Save();
        //        TempData["success"] = "Company Updated Successfully";
        //    }
        //    return RedirectToAction("Index");
        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0) { return NotFound(); }
        //    var Company =_unitOfWork.Company.Get(e=>e.Id == id);
        //    if (Company == null) { return NotFound(); }
        //    return View(Company);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult ConfirmDelete(int? id)
        //{
        //    Company? Company = _unitOfWork.Company.Get(e => e.Id == id);
        //    if(Company == null) { return NotFound(); }
        //    _unitOfWork.Company.Remove(Company);
        //   _unitOfWork.Save();
        //    TempData["success"] = "Company Removed Successfully";
        //    return RedirectToAction("Index");
        //}






        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data = CompanyList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.Get(e => e.Id == id);
            if(obj == null)
            {
                return Json(new { success = false, message = "error whiledeleting" });
            }

            


            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    }
        #endregion

    
}
