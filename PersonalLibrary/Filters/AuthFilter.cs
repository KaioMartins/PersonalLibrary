using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalLibrary.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Session["usuario"] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                //redirecionamento para login:
                filterContext.Result = new RedirectResult("/usuario/login");
                return;
            }
        }
    }
}