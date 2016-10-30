using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class MBController : BaseController
    {
        [test]
        // GET: MB
        public ActionResult Index()
        {

            ViewData["Temp1"] = "Temp1";


            var b = new ClientLoginViewModel()
            {
                FirstName = "Will",
                LastName = "Huang"
            };

            ViewData["Temp2"] = b;

            ViewBag.Temp3 = b;


            return View();
        }

        public ActionResult MyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModel c)
        {
            if (ModelState.IsValid)
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductList()
        {

            var data = db.Product.OrderBy(p => p.ProductId).Take(10);

            return View(data);
        }
        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {
            /*
             * 預設輸出的欄位名稱格式：item.ProductId
            * 要改成以下欄位格式：
              * items[0].ProductId
             * items[1].ProductId
              */
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var product = db.Product.Find(item.ProductId);
                    product.ProductName = item.ProductName;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                    product.Price = item.Price;
                }

                db.SaveChanges();

                return RedirectToAction("ProductList");
            }

            return View();

        }

        public ActionResult MyError()
        {
            throw new InvalidOperationException("ERROR");
            return View();
        }
    }

}