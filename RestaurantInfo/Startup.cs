using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantInfo.Data;

namespace RestaurantInfo
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

            services.AddDbContextPool<RestaurantInfoDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RestaurantInfoDb"));
            });

            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(SayHelloMiddleware);


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules(env);
            app.UseCookiePolicy();
            app.UseMvc();

        }
        /*
         * Poni¿ej mamy przyk³¹dowe Middleware, wymaga on komfortowej pracy z wyra¿eniami lambda i delegatami ale jeœli potrzebujesz "Cross-Cutting Concern" 
         * w Twojej aplikacji, co zawsze bêdzie sprawdzaæ HTTP Header albo ewentualnie ustawiæ Response Header
         * dla ka¿dego Response, to s¹ w³aœnie zachowania które mo¿na zaimplementowaæ z u¿yciem 
         * middleware.
         *  
         *  # Cross-Cutting Concern (zagadnienia przecinaj¹ce)-> Stanowi¹ one te fragmenty kodu, które maj¹ zastosowanie w ca³ej aplikacji, a nie tylko w konkretnej klasie lub metodzie.
         *  wiêcej informacji: https://msdn.microsoft.com/pl-pl/dn890694.aspx
         */
        private RequestDelegate SayHelloMiddleware(
            RequestDelegate arg)
        {
            return async ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello World!");
                }
                else
                {
                    await arg(ctx);
                }
            };
        }
    }
}
