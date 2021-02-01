using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;

namespace canteenVLU.Controllers
{
    public class CONTACTsController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        
        // GET: CONTACTs
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string email,string title,string content)
        {
            var contact = new CONTACT();
            contact.EMAIL = email;
            contact.TITLE = title;
            contact.DATE = DateTime.Now;
            contact.CONTENT = content;
            model.CONTACTs.Add(contact);
            model.SaveChanges();
            return RedirectToAction("Index", "FOODs");
        }
    }
}