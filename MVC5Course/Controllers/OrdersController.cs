using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVC5Course.Controllers
{
    public class OrdersController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        public ActionResult Index(int ClientId)
        {

            var order = db.Order.Include(o => o.Client).Where(p => p.ClientId == ClientId).Take(10);
            return View(order.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}