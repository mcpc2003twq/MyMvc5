﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  找不到路由就導回index頁
        /// </summary>
        /// <param name="actionName"></param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        }
    }
}