namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ForeCastController : Controller
    {
        public ForeCastController()
        {
        }

        [HttpGet]
        public IActionResult Forecast()
        {
            return this.View();
        }
    }
}
