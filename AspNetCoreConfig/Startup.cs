using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig
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
            services.Configure<MyValuesOptions>(Configuration.GetSection("MyStuff:MyValues"));


            // --> Named Configuration
            services.Configure<SiteOption>("Site1", Configuration.GetSection("Credentials:Site1"));
            services.Configure<SiteOption>("Site2", Configuration.GetSection("Credentials:Site2"));


            ////// --> Validation 1
            //services.AddOptions<SiteValidationOptions>()
            //    .Bind(Configuration.GetSection("Credentials:Site1"))
            //    .ValidateDataAnnotations();


            //// --> Validation 2
            //services.AddOptions<SiteValidationOptions>()
            //    .Bind(Configuration.GetSection("Credentials:Site1"))
            //    .Validate(c =>
            //    {
            //        if (c.Url.Contains("site") && string.IsNullOrEmpty(c.Title))
            //            return false;
            //        return true;
            //    }, "Title must be specified when URL contains 'site'");


            //// --> Validation 3
            //services.Configure<SiteValidationOptions>(Configuration.GetSection("Credentials:Site1"));
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<SiteValidationOptions>, SiteValidationOptionsValidation>());


            // --> Validation on Startup
            //services.AddHostedService<ValidateOptionsService>();

            // --> Options Forward Via Interface
            services.Configure<MyValuesConfiguration>(Configuration.GetSection("MyStuff:MyValues"));
            services.AddSingleton<IMyValuesConfiguration>(s =>
                s.GetRequiredService<IOptions<MyValuesConfiguration>>().Value);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
