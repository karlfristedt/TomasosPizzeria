using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IUsers, UserRepository>();

            services.AddDbContext<TomasosDBContext>(options => options.UseSqlServer(Configuration["Data:TomasosPizzeria:ConnectionString"]));
            services.AddScoped<Kundvagn>(sp => SessionKundvagn.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddDistributedMemoryCache(); 
            services.AddSession();

            services.AddDbContext<TomasosIdentityDBContext>(options => options.UseSqlServer(Configuration["Data:TomasosPizzeriaIdentity:ConnectionString"]));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TomasosIdentityDBContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseAuthentication();

            TomasosIdentityDBContext.CreateRoles(app.ApplicationServices).Wait();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=ShowUsers}/{id?}");
            });

        }
    }
}
