using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspcore_watchshop.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspcore_watchshop {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllersWithViews();
            services.AddDbContext<watchContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("default")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.Use (async (context, next) => {
                await next ();
                if (context.Response.StatusCode == 404) {
                    context.Request.Path = "/Home/Error";
                    await next ();
                }
            });

            app.UseHttpsRedirection ();

            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "trang-chu",
                    pattern: "/",
                    defaults : new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute (
                    name: "error",
                    pattern: "/error",
                    defaults : new { controller = "Home", action = "Error" });
                endpoints.MapControllerRoute (
                    name: "card",
                    pattern: "/card",
                    defaults : new { controller = "Cart", action = "Index" });
                endpoints.MapControllerRoute (
                    name: "san-pham",
                    pattern: "/san-pham",
                    defaults : new { controller = "Product", action = "Detail" });
                endpoints.MapControllerRoute (
                    name: "san-pham",
                    pattern: "/{action}",
                    defaults : new { controller = "Product", action = "Discount" });
                endpoints.MapControllerRoute (
                    name: "product",
                    pattern: "/{controller}/{action}");
                endpoints.MapControllerRoute (
                    name: "not-found",
                    pattern: "/{controller}/{action}",
                    defaults : new { controller = "Home", action = "Error" });
            });

        }
    }
}