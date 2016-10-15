using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Scrutor;
using Web.Features.Home;
using Money.Accounts.Models;
using Money.Infrastructure;
using Web;

namespace Web
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(
        optionsBuilder => optionsBuilder.UseInMemoryDatabase());
      
      // Add framework services.
      services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
        .AddRazorOptions(options => 
        {
          options.ViewLocationFormats.Clear();
          options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
          options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
          options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

          options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
        });

      // Add application services.
      services.AddScoped<SingleInstanceFactory>(p => t => p.GetRequiredService(t));

      services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator), typeof(HomeController))
                .AddClasses()
                .AsImplementedInterfaces());

      services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator), typeof(AccountOverview))
                .AddClasses()
                .AsImplementedInterfaces());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }
      else
      {
          app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
          routes.MapRoute(
              name: "default",
              template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}