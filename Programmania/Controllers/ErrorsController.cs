using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : Controller
{
    [AllowAnonymous]
    public IActionResult Index(int statusCode)
    {
        return View($"/Views/Errors/Error{statusCode}.cshtml");
    }
}