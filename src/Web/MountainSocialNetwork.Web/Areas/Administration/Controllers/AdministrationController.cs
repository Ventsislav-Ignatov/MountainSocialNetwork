namespace MountainSocialNetwork.Web.Areas.Administration.Controllers
{
    using MountainSocialNetwork.Common;
    using MountainSocialNetwork.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
