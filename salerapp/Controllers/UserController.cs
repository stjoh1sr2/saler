using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Controllers
{
    public class UserController : Controller
    {
        public ActionResult LogOut()
        {
            if (HttpContext.Session.GetString("_User") is not null)
            {
                HttpContext.Session.Remove("_User");
            }

            return Redirect("~/");
        }
    }
}
