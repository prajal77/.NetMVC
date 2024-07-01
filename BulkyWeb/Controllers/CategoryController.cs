using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
        _db = db;
        }
        public IActionResult Index()
        {
            //Category list will be retrieved 
            List<Category> objCategoryList = _db.Categories.ToList();  
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Costume validation
            /*if (obj.Name == obj.DisplayOrder.ToString() )
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
                // Clear model state to prevent model binding
                ModelState.Clear();
             }*/
            if (ModelState.IsValid)
            {
               
                _db.Categories.Add(obj);
                _db.SaveChanges();
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
            Category? categoryFromDb = _db.Categories.Find(id);
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

                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            Category? categoryFromDb = _db.Categories.Find(id);
          
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
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
                {
                return NotFound();
               
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

         
        }

    }
}
