namespace MountainSocialNetwork.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Common;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Services.Data.Administrator;
    using MountainSocialNetwork.Web.ViewModels.Administration.Dashboard;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private static IAdministratorService administratorService;

        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
            : base(administratorService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
