namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Common;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Services.Messaging;
    using MountainSocialNetwork.Web.ViewModels.Contact;

    public class ContactsController : Controller
    {
        private readonly IContactService contactService;
        private readonly IEmailSender emailSender;

        public ContactsController(IContactService contactService, IEmailSender emailSender)
        {
            this.contactService = contactService;
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.contactService.CreateAsync(
                viewModel.Name,
                viewModel.Email,
                viewModel.Title,
                viewModel.Content);

            await this.emailSender.SendEmailAsync(
               viewModel.Email,
               viewModel.Name,
               GlobalConstants.SystemEmail,
               viewModel.Title,
               viewModel.Content);

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
