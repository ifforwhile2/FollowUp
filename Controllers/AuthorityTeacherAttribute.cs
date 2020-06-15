using System;
using System.Web.Mvc;

namespace FallowUP.Controllers
{
    internal class AuthorityTeacherAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Yetki"] == "2")
            {
                base.OnActionExecuting(filterContext);

            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}