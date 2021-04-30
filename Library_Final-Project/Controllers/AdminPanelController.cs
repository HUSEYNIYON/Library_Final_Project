using Library_Final_Project.Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Final_Project.Controllers
{
    public class AdminPanelController : Controller
    {
        [Authorize(Roles = nameof(Roles.Admin))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
