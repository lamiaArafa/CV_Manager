using ArtStudio.Application;
using ArtStudio.Application.Common.Exceptions;
using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Infrastructure.Presistence;
using ArtStudio.Infrastructure.External;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;
using System.Configuration;
using Serilog.Formatting.Json;
using ArtStudio.Web.Hosted;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);

        var app = builder.Build();

        ConfigureSerilog(app);

        ConfigureDataBaseMigration(app);

        ConfigureHTTPPipeline(app);
       
        app.Run();
    }

    private static void ConfigureHTTPPipeline(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseDeveloperExceptionPage();
        }

        ConfigureExceptionMiddleware(app);

        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Client}/{action=Index}");
    }

    private static void ConfigureSerilog(WebApplication app)
    {
        app.UseSerilogRequestLogging();
        Log.Logger = CreateCustomeLogger();

        //Log.Logger = new LoggerConfiguration()
        //  .ReadFrom.Configuration(app.Configuration)
        //  .WriteTo.File()
        //  .CreateLogger();


    }

    private static void ConfigureDataBaseMigration(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }

    private static void ConfigureExceptionMiddleware(WebApplication app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features
                .Get<IExceptionHandlerPathFeature>()?
                .Error;

                if (exception is BadRequestException applicationException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var erroResult = Result<string>.Failure(applicationException.Message);
                    await context.Response.WriteAsJsonAsync(erroResult);
                    return;
                }


                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = Result<string>.Failure("Internal Server Error", exception ?? new Exception("unhandled exception"));

                await context.Response.WriteAsJsonAsync(result);
            });
        });
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        //builder.Services.AddSingleton<System.Threading.Timer>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        builder.Services.AddSerilog();

        builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilterAttribute>();
        });

        builder.Services.AddPresistanceServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddApplicationLayer();


        builder.Services.AddScoped<WhatsAppHostingService>();
        builder.Services.AddHostedService<WhatsAppHostingService>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });

    }

    public static Logger CreateCustomeLogger()
    {
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.HealthChecks", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Extensions.Diagnostics.HealthChecks", LogEventLevel.Warning)
            .MinimumLevel.Override("AspNetCore.HealthChecks.UI", LogEventLevel.Warning)
            .MinimumLevel.Override("HealthChecks", LogEventLevel.Warning)
            .MinimumLevel.Override("Hangfire", LogEventLevel.Information)
            .Enrich.FromLogContext()

            //.Enrich.WithClientIp()

            //.Filter.ByExcluding(logEvent =>
            //{
            //    return logEvent.Exception != null &&
            //    (logEvent.Exception.GetType() == typeof(BadeRequestException));
            //})

            //   .Filter.ByExcluding("RequestPath like '/health%'")

            .Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore.InMemory"))

            //.WriteTo.Console()
            .WriteTo.File(formatter: new JsonFormatter(), "Logs/log-.json", rollingInterval: RollingInterval.Day);


        //notify developers by email when exception happens

        //if (appSettings.DevelopersEmails != null && appSettings.DevelopersEmails.Count > 0)
        //{
        //    loggerConfiguration.WriteTo.Email(new EmailConnectionInfo
        //    {
        //        FromEmail = appSettings.LoggingSMTPConfiguration.Mail,
        //        ToEmail = string.Join(",", appSettings.DevelopersEmails),
        //        MailServer = appSettings.LoggingSMTPConfiguration.Host,
        //        NetworkCredentials = new NetworkCredential
        //        {
        //            UserName = appSettings.LoggingSMTPConfiguration.Mail,
        //            Password = appSettings.LoggingSMTPConfiguration.Password,
        //        },
        //        EnableSsl = false,
        //        Port = appSettings.LoggingSMTPConfiguration.Port,
        //        EmailSubject = "Clients-App Exception"
        //    }, restrictedToMinimumLevel: LogEventLevel.Error, batchPostingLimit: 5);
        //}

        //logging request activity

        //var requestLogs = Matching.FromSource("Serilog.AspNetCore.RequestLoggingMiddleware");
        //loggerConfiguration
        //    .WriteTo.Logger(c =>
        //            c.Filter.ByIncludingOnly(requestLogs)
        //            .WriteTo.File("Logs/Requestactivity-.txt"
        //            , LogEventLevel.Information
        //            , rollingInterval: RollingInterval.Day
        //            , outputTemplate: "[{Timestamp:o}] [{Level:u3}]  [{UserEmail}]  [{UserRole}] [{ClientIp}]  {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms {NewLine}")
        //    );

        return loggerConfiguration
            .CreateLogger();
    }
}