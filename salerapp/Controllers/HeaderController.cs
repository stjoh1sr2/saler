using Microsoft.AspNetCore.Mvc;
using salerapp.Helpers;

namespace salerapp.Controllers
{
    public class HeaderController : Controller
    {
        public ActionResult LogOut()
        {
            UserManagementHelper.LogOut();

            return Redirect("~/");
        }
    }
}
