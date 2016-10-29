using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class testAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //如果進來的人是local,就導頁回首頁
            if (filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}