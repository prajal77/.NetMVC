using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db) 
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            //Category list will be retrieved 
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();  
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
          
            if (ModelState.IsValid)
            {

                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //Can only work on primary Key
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id ==id);
            //Can work on any 
          /*  Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault() ;*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
           
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Can only work on primary Key
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
          
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        // if you want to use id only cannot use same name as parameter is same
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if(obj == null)
                {
                return NotFound();
               
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

         
        }

    }
}
