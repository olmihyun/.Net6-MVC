using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder는 Name과 달라야합니다.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("name", "값을 입력해주세요.");
                ModelState.AddModelError("DisplayOrder", "값을 입력해주세요.");
            }

            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFormDb = _db.Categories.Find(id);
            //var categoryFormDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFormDb == null)
            {
                return NotFound();
            }

            return View(categoryFormDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder는 Name과 달라야합니다.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "카테고리 수정이 완료되었습니다.";

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("name", "값을 입력해주세요.");
                ModelState.AddModelError("DisplayOrder", "값을 입력해주세요.");
            }

            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFormDb = _db.Categories.Find(id);
            //var categoryFormDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFormDb == null)
            {
                return NotFound();
            }

            return View(categoryFormDb);
        }

        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "카테고리 삭제가 완료되었습니다.";

            return RedirectToAction("Index");
        }


    }//end class
}
