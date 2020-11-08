using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MountainSocialNetwork.Data;
using MountainSocialNetwork.Data.Models;

[assembly: HostingStartup(typeof(MountainSocialNetwork.Web.Areas.Identity.IdentityHostingStartup))]
namespace MountainSocialNetwork.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}