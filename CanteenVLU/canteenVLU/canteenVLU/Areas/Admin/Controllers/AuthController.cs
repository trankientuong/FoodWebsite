using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;
namespace canteenVLU.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        AD1Team5Entities model = new AD1Team5Entities();
        public ActionResult Login()
        {
            Session["password-incorrect"] = false;
            Session["user-not-found"] = false;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = model.ACCOUNTs.FirstOrDefault(u => u.EMAIL.Equals(email));
            if(user != null)
            {
                
                if (user.PASSWORD.Equals(password))
                {
                    Session["user-Fullname"] = user.FULL_NAME;
                    Session["user-ID"] = user.ID;
                    Session["user-role"] = user.ROLE;               
                    if ((int)Session["user-role"] == 1)
                    {                       
                        return RedirectToAction("Index", "Admin");

                    }
                    else 
                    {
                        return RedirectToAction("Index", "FOOD");
                    }
                   
                    
                }
              
                else
                {
                    Session["password-incorrect"] = true;
                    return View();
                }            
            }
            else
            {
                Session["user-not-found"] = true;
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["user-Fullname"] = null;
            Session["user-ID"] = null;         
            return RedirectToAction("Login");
        }
    }
}