using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scrutor;
using Money.Web.Features.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Money.Core.Identity.Domain;

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
      //services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase());
      
      // Add framework services.
      services.AddMvc(options => 
      {
        var policy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();
                 
        options.Filters.Add(new AuthorizeFilter(policy));
        options.Conventions.Add(new FeatureConvention());
      })
      .AddRazorOptions(options => 
      {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

        options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
      });

      // Add application services.
      //services.AddScoped<SingleInstanceFactory>(p => t => p.GetRequiredService(t));

      services.Scan(scan => scan
        .FromAssembliesOf(
          typeof(HomeController),  // Money.Web
          typeof(User))            // Money.Core
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

      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
          LoginPath = new PathString("/auth/login"),
          AccessDeniedPath = new PathString("/auth/login"),
          AutomaticAuthenticate = true,
          AutomaticChallenge = true
      });
      
      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}