using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using TechChallenge.DataAccess;
using TechChallenge.BusinessLogic;

namespace TechChallenge
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc();

            //Adds DbContext as a service. Using an In-Memory database to keep the things simple.
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TechChallenge_DB"));            
            //Adds BooksLogic as a service.
            services.AddTransient<IBooksLogic, BooksLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            /* Seeds the database. Called here to be sure that the seeding only happens once. Thanks to
            https://stackoverflow.com/questions/46547108/asp-net-core-2-using-in-memory-database-for-prototyping-getting-cannot-resolve-s*/
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
