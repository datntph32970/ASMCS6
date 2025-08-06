using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
        var tempData = factory?.GetTempData(context.HttpContext);

        if (context.Exception is UnauthorizedAccessException ex)
        {
            if (tempData != null)
            {
                tempData["ToastMessage"] = ex.Message;
                tempData["ToastType"] = "danger";
            }
            context.Result = new RedirectToActionResult("Login", "Account", null);
            context.ExceptionHandled = true;
        }
        else
        {
            if (tempData != null)
            {
                tempData["ToastMessage"] = "Đã xảy ra lỗi: " + context.Exception.Message;
                tempData["ToastType"] = "danger";
            }
            context.Result = new RedirectToActionResult("Error", "Home", null);
            context.ExceptionHandled = true;
        }
    }
}