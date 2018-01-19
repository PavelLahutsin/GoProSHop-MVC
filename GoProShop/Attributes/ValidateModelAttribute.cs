using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GoProShop.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var modelState = filterContext.Controller.ViewData.ModelState;

                if (!modelState.IsValid)
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    var errors = modelState.Where(m => m.Value.Errors.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors
                                .FirstOrDefault().ErrorMessage)
                               ;

                    filterContext.Result = new JsonResult { Data = errors };
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}