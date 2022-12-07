using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Interfaces;
using IntegrationLibrary.Core;
using IntegrationLibrary.News;
using IntegrationLibrary.News.Interfaces;
using IntegrationLibrary.Notification;
using IntegrationLibrary.Notification.Interfaces;
using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender;
using IntegrationLibrary.Tender.Interfaces;
using IntegrationLibrary.Util;
using IntegrationLibrary.Util.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mjml.AspNetCore;

namespace IntegrationAPI
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
            services.AddDbContext<IntegrationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("HospitalDb")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegrationAPI", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddLogging();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBloodBankService, BloodBankService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IMailSender, MailSender>();
            services.AddScoped<IBBConnections, BBConnections>();
            services.AddMjmlServices(o =>
            {
                o.DefaultKeepComments = true;
                o.DefaultBeautify = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntegrationAPI v1"));
            }

            /*app.UseMiddleware<APIKeyMiddleware>(new APIKeyOptions
            {
                Endpoints = new List<string> { @"/api/BloodBank/all" }
            });*/

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
