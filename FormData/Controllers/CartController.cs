using FormData.DataLayer;
using FormData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormData.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddToCart(CartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            Cart sc = new Cart();
            sc.ProductID = cartDTO.ProductId;
            sc.CustomerID = cartDTO.CustomerId;
            sc.Quantity = cartDTO.Quantity;

            // save changes

            using (var db = new NorthwndEntities())
            {
                if (db.Carts.Any(c=> c.ProductID == sc.ProductID && c.CustomerID == sc.CustomerID))
                {
                    // DON'T DO IT THIS WHAY.
                    // best way to do it it's with injections
                    // iNterface injections.
                    // create an iNterface of the Cart class and inject it were it need it.
                    Cart cart =  db.Carts.FirstOrDefault(c => c.ProductID == sc.ProductID);
                    cart.Quantity += sc.Quantity;
                    sc = new Cart()
                    {
                        // DON'T DO IT THIS WHAY.
                        // best way to do it it's with injections
                        // iNterface injections.
                        // create an iNterface of the Cart class and inject it were it need it.
                        CartID = cart.CartID,
                        ProductID = cart.ProductID,
                        CustomerID = cart.CustomerID,
                        Quantity = cart.Quantity
                    };
                }
                else
                {
                    db.Carts.Add(sc);
                }



                db.SaveChanges();
            }

            return Json(sc, JsonRequestBehavior.AllowGet);
        }
    }
}