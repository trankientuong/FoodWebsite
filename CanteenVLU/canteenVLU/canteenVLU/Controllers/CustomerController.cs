using canteenVLU.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace canteenVLU.Controllers
{
    public class CustomerController : Controller
    {
       AD1Team5Entities model = new AD1Team5Entities();
        // GET: Customer
        public ActionResult Login()
        {
            Session["password-incorrect"] = false;
            Session["customer-not-found"] = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email,string password)
        {
            var customer = model.CUSTOMERs.FirstOrDefault(u => u.EMAIL.Equals(email));
            if (customer != null)
            {
                if (customer.PASSWORD.Equals(password))
                {
                    Session["customer-fullname"] = customer.FULL_NAME;
                    Session["customer-email"] = customer.EMAIL;
                    Session["customer-phone"] = customer.PHONE_NUMBER;
                    Session["customer-faculty"] = customer.FACULTY_ID;
                    Session["customer-ID"] = customer.ID;
                    return RedirectToAction("Index","FOODs");
                }
                else
                {
                    Session["password-incorrect"] = true;
                    return View();
                }
            }
            else
            {
                Session["customer-not-found"] = true;
                return View();
            }

        }


        public ActionResult Create()
        {
            ViewBag.Faculty_type = model.FACULTies.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        private bool checkEmailExist(string email)
        {
            return model.CUSTOMERs.FirstOrDefault(x => x.EMAIL.Equals(email)) != null ? true : false;
        }

        [HttpPost]
        public ActionResult Create(string email,string password,string fullname,string phone,string customertype,int faculty)
        {
            
            if (ModelState.IsValid)
            {
                
                if (checkEmailExist(email))
                {
                    Session["email-exist"] = true;
                    return View();
                }
                else
                {
                    var customer = new CUSTOMER();
                    customer.EMAIL = email;
                    customer.PASSWORD = password;
                    customer.FULL_NAME = fullname;
                    customer.PHONE_NUMBER = phone;
                    customer.STATUS = true;
                    customer.CUSTOMER_TYPE = 1;
                    customer.FACULTY_ID = faculty;
                    model.CUSTOMERs.Add(customer);
                    model.SaveChanges();
                    return RedirectToAction("Login", "Customer");
                }
            }
            
            return View();
        }






        public ActionResult Logout()
        {
            Session["customer-fullname"] = null;
            Session["customer-ID"] = null;
            return RedirectToAction("Index","FOODs");
        }


    }
}