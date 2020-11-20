using System;
using System.Data.Common;
using System.IO;
using Autofac;
using Expertec.Sigeco.CrossCutting.EventBus;
using Expertec.Sigeco.CrossCutting.EventBusServiceBus;
using Expertec.Sigeco.CrossCutting.IntegrationEventLog.Services;
using Expertec.Sigeco.CrossCutting.LoggingEvent;
using Expertec.Lcc.Services.Notify.API.Hubs;
using Expertec.Lcc.Services.Notify.API.Infrastructure;
using Expertec.Lcc.Services.Notify.API.Infrastructure.AutofacModules;
using Expertec.Lcc.Services.Notify.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.DataProtection;
using Expertec.Lcc.Services.Notify.API.IntegrationEvents.EventHandling;
using Expertec.Sigeco.CrossCutting.EventBus.Abstractions;
using Expertec.Lcc.Services.Notify.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Http.Connections;

namespace Expertec.Lcc.Services.Notify.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private const string XForwardedPathBase = "X-Forwarded-PathBase";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"/var/lib/dataprotected/"));

            services.AddDbContext<NotifyContext>(options =>
                 options.UseSqlServer(Configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(60),
                            errorNumbersToAdd: null
                        );
                    } 
                ));

            services.AddDbContext<LogContext>(options =>
                 options.UseSqlServer(Configuration["ConnectionString"]));

            services.Configure<NotifySettings>(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = "sigecoservices";
                options.Authority = Configuration["IdentityUrl"];

                options.RequireHttpsMetadata = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    cors => cors.WithOrigins("http://localhost:3000", "http://lcc.x-pertec.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NotifyAPI",
                    Version = "v1",
                    Description = "Especificación de servicios para notificaciones.",
                    Contact = new OpenApiContact { Name = "Expert Tecnologies SAC", Email = "jabanto@x-pertec.com", Url = new Uri("https://x-pertec.com") }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "notifyapi.xml"); 
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSignalR();

            services.AddControllers();
            
            //configure servicebus
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c)
            );

            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(Configuration["EventBusConnection"]);
                serviceBusConnection.EntityPath = Configuration["TopicName"];

                return new DefaultServiceBusPersisterConnection(serviceBusConnection);
            });

            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var subscriptionClient = new SubscriptionClient(serviceBusPersisterConnection.ServiceBusConnectionStringBuilder, subscriptionClientName);
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();

                return new EventBusServiceBus(serviceBusPersisterConnection, eventBusSubcriptionsManager, subscriptionClient, iLifetimeScope);

            });

            services.AddTransient<UsuarioCreadoIntegrationEventHandler>();
            services.AddTransient<UsuarioEliminadoIntegrationEventHandler>();
            services.AddTransient<UsuarioModificadoIntegrationEventHandler>();
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use((context, next) =>
            {
                if(context.Request.Headers.TryGetValue(XForwardedPathBase, out Microsoft.Extensions.Primitives.StringValues pathBase ))
                {
                    context.Request.PathBase = new PathString(pathBase);
                }

                return next();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint(Configuration["SwaggerEndPoint"], "Notify Api V1");
                   c.SupportedSubmitMethods(new SubmitMethod(){});
               });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );
                endpoints.MapHub<NotificacionHub>("/notificacion", options =>
                {
                    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                });
            });

            app.UseFileServer();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UsuarioCreadoIntegrationEvent, UsuarioCreadoIntegrationEventHandler>();
            eventBus.Subscribe<UsuarioEliminadoIntegrationEvent, UsuarioEliminadoIntegrationEventHandler>();
            eventBus.Subscribe<UsuarioModificadoIntegrationEvent, UsuarioModificadoIntegrationEventHandler>();
        }
    }
}
