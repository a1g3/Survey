using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Controllers;
using Survey.Domain.Infastructure;
using Survey.Domain.Interfaces.Infastructure;
using Survey.Helpers;

namespace Survey
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
            services.AddMvc().AddViewOptions(opt => opt.HtmlHelperOptions.ClientValidationEnabled = false).AddControllersAsServices();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly(), Assembly.Load("Survey.Domain"), Assembly.Load("Survey.Data")).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterInstance(new SurveySettings(Configuration.GetSection("ControlQuestions").Get<List<int>>())).As<ISurveySettings>().SingleInstance();

            //Controllers
            builder.RegisterType<HomeController>().PropertiesAutowired();
            builder.RegisterType<QuestionController>().PropertiesAutowired();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            AutoMapperConfig.InitializeAutoMapper();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
