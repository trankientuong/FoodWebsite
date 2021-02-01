using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;

namespace canteenVLU.Controllers
{
    public class ShoppingCartController : Controller
    {
        AD1Team5Entities db = new AD1Team5Entities();
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

        // GET: ShoppingCart
        public ActionResult Index()
        {
            GetShoppingCart();
            var hashtable = new Hashtable();
            foreach (var billDetail in ShoppingCart)
            {
                if (hashtable[billDetail.MENU.ID] != null)
                {
                    (hashtable[billDetail.MENU.ID] as ORDER_DETAIL).QUANTITY += billDetail.QUANTITY;
                }
                else hashtable[billDetail.MENU.ID] = billDetail;
            }
            ShoppingCart.Clear();
            foreach (ORDER_DETAIL billDetail in hashtable.Values)
            {
                ShoppingCart.Add(billDetail);
            }

            return View(ShoppingCart);
        }

        [HttpPost]
        // GET: ShoppingCart/Create
        public ActionResult Create(int menuId, int quantity)
        {
            GetShoppingCart();
            var menu = db.MENUs.Find(menuId);
            ShoppingCart.Add(new ORDER_DETAIL
            {
                MENU = menu,
                QUANTITY = quantity
            });
            return RedirectToAction("Index");
        }


        [HttpPost]      
        public ActionResult Edit(int[] menu_id, int[] quantity)
        {
            GetShoppingCart();
            ShoppingCart.Clear();
            if (menu_id != null)
                for (int i = 0; i < menu_id.Length; i++)
                    if (quantity[i] > 0)
                    {
                        var menu = db.MENUs.Find(menu_id[i]);
                        ShoppingCart.Add(new ORDER_DETAIL
                        {
                            MENU = menu,
                            QUANTITY = quantity[i]
                        });
                    }
                    else if(quantity[i] == 0)
                    {
                        var menu = db.MENUs.Find(menu_id[i]);
                        ShoppingCart.Remove(new ORDER_DETAIL
                        {
                            MENU = menu,
                            QUANTITY = quantity[i]
                        });
                    }
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            GetShoppingCart();
            ShoppingCart.Clear();
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }

        // GET: ShoppingCart/Delete/5    

        public ActionResult Delete(int id)
        {           
            GetShoppingCart();            
            int index = isExist(id);
            ShoppingCart.RemoveAt(index);
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            GetShoppingCart();
            for (int i = 0; i < ShoppingCart.Count; i++)
                if (ShoppingCart[i].MENU.ID.Equals(id))
                    return i;
            return -1;
        }



       


    }
}
