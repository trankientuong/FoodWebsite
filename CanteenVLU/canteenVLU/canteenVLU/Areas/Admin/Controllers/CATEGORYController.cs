using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using canteenVLU.Models;
using WebCanteen.Areas.Admin.Middleware;
namespace canteenVLU.Areas.Admin.Controllers
{
    [LoginVerification]

    public class CATEGORYController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        // GET: Admin/CATEGORY

        public ActionResult Index()
        {
            var category = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CATEGORY c, HttpPostedFileBase picture)
        {

            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    var fileName = Path.GetFileName(picture.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Images/"));
                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    picture.SaveAs(pathToSave);
                    c.IMAGE_URL = Path.Combine("/Images/",fileName);
                }
                model.CATEGORies.Add(c);
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CATEGORY c, HttpPostedFileBase picture)
        {

            var fileName = Path.GetFileName(picture.FileName);
            var directoryToSave = Server.MapPath(Url.Content("~/Images/"));
            var pathToSave = Path.Combine(directoryToSave, fileName);
            picture.SaveAs(pathToSave);
            c.IMAGE_URL = Path.Combine("/Images/",fileName);



            model.Entry(c).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var category = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            model.CATEGORies.Remove(category);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var category = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            return View(category);
        }

    }
}