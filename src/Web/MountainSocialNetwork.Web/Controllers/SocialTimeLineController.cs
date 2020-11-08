namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class SocialTimeLineController : Controller
    {
        public IActionResult Timeline()
        {
            return this.View();
        }
    }
}
