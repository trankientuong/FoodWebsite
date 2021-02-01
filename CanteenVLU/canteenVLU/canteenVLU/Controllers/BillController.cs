using canteenVLU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace canteenVLU.Controllers
{
    public class BillController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        private List<ORDER_DETAIL> ShoppingCart = null;


        private void GetShoppingCart()
        {
            //var session = System.Web.HttpContext.Current.Session;
            if (Session["ShoppingCart"] != null)
                ShoppingCart = Session["ShoppingCart"] as List<ORDER_DETAIL>;
            else
            {
                ShoppingCart = new List<ORDER_DETAIL>();
                Session["ShoppingCart"] = ShoppingCart;
            }
        }

        // GET: Bill
     

        public ActionResult Create()
        {           
            GetShoppingCart();
            ViewBag.Cart = ShoppingCart;          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ORDER order,int status)
        {
            GetShoppingCart();
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    order.DATE = DateTime.Now;
                    order.STATUS = status;                   
                    order.ACCOUNT_ID = 1;
                    order.CUSTOMER_ID = (int)Session["customer-ID"];
                    model.ORDERs.Add(order);
                    model.SaveChanges();

                    foreach (var item in ShoppingCart)
                    {
                        model.ORDER_DETAIL.Add(new ORDER_DETAIL
                        {
                            ORDER_ID = order.ID,
                            MENU_ID = item.MENU.ID,
                            PRICE = item.MENU.PRICE,
                            QUANTITY = item.QUANTITY

                        });
                    }
                    model.SaveChanges();
                    scope.Complete();
                    Session["ShoppingCart"] = null;
                    return RedirectToAction("Index", "FOODs");
                }
            }
            GetShoppingCart();
            ViewBag.Cart = ShoppingCart;
            return View(order);
        }
    }
}