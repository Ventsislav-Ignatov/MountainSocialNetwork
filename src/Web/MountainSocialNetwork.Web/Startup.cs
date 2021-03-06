﻿namespace MountainSocialNetwork.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Common;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Data.Seeding;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Services.Data.Administrator;
    using MountainSocialNetwork.Services.Data.Friend;
    using MountainSocialNetwork.Services.Data.NewsFeed;
    using MountainSocialNetwork.Services.Data.Search;
    using MountainSocialNetwork.Services.Data.Votes;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Services.Messaging;
    using MountainSocialNetwork.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                      options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IAdministratorService, AdministratorService>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<INewsFeedService, NewsFeedService>();
            services.AddTransient<INewsFeedCommentService, NewsFeedCommentService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IArticlePostService, ArticlePostsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IArticleHomePageService, ArticleHomePageService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            services.AddTransient<IFriendService, FriendService>();
            services.Configure<MailKitEmailSenderOptions>(this.configuration.GetSection("SmtpSettings"));

            // Cloudinary
            Account cloudinaryCredentials = new Account(
              this.configuration["Cloudinary:CloudName"],
              this.configuration["Cloudinary:ApiKey"],
              this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            // Facebook Login
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = this.configuration["Authentication:Facebook:AppSecret"];
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=NewsFeed}/{action=NewsFeedContent}/{id?}");
                    endpoints.MapRazorPages();
                    });
        }
    }
}
