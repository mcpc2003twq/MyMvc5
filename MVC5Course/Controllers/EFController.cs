using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF

        FabricsEntities db = new FabricsEntities();
        public ActionResult Index()
        {

            var data = db.Product.Take(20);

            return View(data);
        }
        public ActionResult Create()
        {

            var product = new Product()
            {
                ProductName = "AAA",
                Active = true,
                Price = 100,
                Stock = 5

            };

            db.Product.Add(product);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);

            db.Product.Remove(product);

            db.SaveChanges();

            return View();
        }

        public ActionResult Details(int id)
        {
            var data = db.Product.Find(id);

            return View(data);
        }


        //public ActionResult Update(int id)
        //{
        //    var data = db.Product.Find(id);

        //    data.ProductName = data.ProductName + "!";

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        foreach (var entityErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var vErrors in entityErrors.ValidationErrors)
        //            {
        //                throw new DbEntityValidationException(vErrors.PropertyName + "發生錯誤:" + vErrors.ErrorMessage);
        //            }
        //        }
        //    }


        //    return RedirectToAction("Index");
        //}

        public ActionResult Add20percent()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));

            foreach (var item in data)
            {
                if (item.Price.HasValue)
                {
                    item.Price = item.Price * 1.2m;
                }
            }

            db.SaveChanges();

            return View();
        }
    }
}