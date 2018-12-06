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
                db.Carts.Add(sc);
                db.SaveChanges();
            }

            return Json(sc, JsonRequestBehavior.AllowGet);
        }
    }
}