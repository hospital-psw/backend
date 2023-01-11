using IntegrationAPI.Token;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Interfaces;
using IntegrationLibrary.Core;
using IntegrationLibrary.News;
using IntegrationLibrary.News.Interfaces;
using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender;
using IntegrationLibrary.Tender.Interfaces;
using IntegrationLibrary.UrgentBloodTransfer;
using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
using IntegrationLibrary.Util;
using IntegrationLibrary.Util.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mjml.AspNetCore;
using System.Collections.Generic;
using System.Text;

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
                c.CustomSchemaIds(x => x.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthorization();
            services.AddAutoMapper(typeof(Startup));
            services.AddLogging();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBloodBankService, BloodBankService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<IUrgentBloodTransferService, UrgentBloodTransferService>();
            services.AddScoped<IUrgentBloodTransferStatisticsService, UrgentBloodTransferStatisticsService>();


            services.AddScoped<IHTMLReportService, HTMLReportService>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IMailSender, MailSender>();
            services.AddScoped<IBBConnections, BBConnections>();
            services.AddScoped<ISFTPService, SFTPService>();

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
