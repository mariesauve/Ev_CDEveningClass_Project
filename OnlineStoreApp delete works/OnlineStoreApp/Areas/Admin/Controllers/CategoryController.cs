using MerchProjectDataAccess.Data;
using MerchProjectDataAccess.Repository.IRepository;
using MerchProjectModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new();
            if (id == null)
            {
                return View(category);
            }
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //   ^^^^^ Prevents Cross site forgery
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);

                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            //if (objFromDb != null) ??
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion


    }
}
