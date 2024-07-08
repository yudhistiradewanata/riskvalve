using Microsoft.AspNetCore.Mvc;

namespace Riskvalve.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        ViewBag.ErrorMessage = statusCode switch
        {
            400 => "Bad request error",
            _ => "Error occurred",
        };
        return View("StatusCodePage");
    }
}