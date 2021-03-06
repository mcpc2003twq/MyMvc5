﻿using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
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

            var data = db.Product.Where(p => p.ProductName.Contains("AAA"));

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

            return RedirectToAction("Index");
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
            string str = "%AAA%";
            db.Database.ExecuteSqlCommand("Update dbo.product set price=price*1.2 where productName like @p0", str);
            //var data = db.Product.Where(p => p.ProductName.Contains("AAA"));

            //foreach (var item in data)
            //{
            //    if (item.Price.HasValue)
            //    {
            //        item.Price = item.Price * 1.2m;
            //    }
            //}

            //db.SaveChanges();

            return RedirectToAction("Index");
        }
        /// <summary>
        /// 讀取view呈現資料
        /// </summary>
        /// <returns></returns>
        public ActionResult ClientContribution()
        {

            var data = db.vw_ClientContribution.Take(20);

            return View(data);
        }
        /// <summary>
        /// 使用inlineSQL取得資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ClientContribution2(string id)
        {
            //string keyword = "%Mary%";
            var data = db.Database.SqlQuery<ClientContributionViewModel>("SELECT " +
             "c.ClientId,c.FirstName,c.LastName,(SELECT SUM(o.OrderTotal) FROM[dbo].[Order] o " +
             "WHERE o.ClientId = c.ClientId) as OrderTotal FROM [dbo].[Client] as c " +
             "where c.FirstName like @p0", id);


            return View(data);
        }

        /// <summary>
        ///使用sp取得資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ClientContribution3(string id)
        {
            var data = db.usp_GetClientContribution(id);

            return View(data);
        }
    }
}