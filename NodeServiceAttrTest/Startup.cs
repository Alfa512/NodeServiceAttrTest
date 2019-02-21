using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodeServiceAttrTest.Contracts;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Contracts.Services;
using NodeServiceAttrTest.Data;
using NodeServiceAttrTest.Data.Repositories;
using NodeServiceAttrTest.Services;

namespace NodeServiceAttrTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.ConfigureApplicationCookie(options =>
            {
                services.AddOptions();
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IServiceNodesRepository, ServiceNodesRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<INodeRepository, NodeRepository>();
            services.AddTransient<IAttributeRepository, AttributeRepository>();
            services.AddTransient<ICustomDataSetRepository, CustomDataSetRepository>();
            services.AddTransient<IDataContext, ApplicationContext>();
            services.AddTransient<INodeService, NodeService>();
            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IAttributeService, AttributeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {
                using (var context = new ApplicationContext(new ConfigurationService(Configuration)))
                {
                    context.Database.Migrate();
                }

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<ApplicationContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
            catch (Exception)
            {
                //ignore
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}