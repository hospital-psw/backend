using HospitalAPI.Configuration;
using HospitalAPI.EmailServices;
using HospitalAPI.Mappers;
using HospitalAPI.TokenServices;
using HospitalLibrary.Core.Model.ApplicationUser;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.Blood;
using HospitalLibrary.Core.Service.Blood.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace HospitalAPI
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
            services.AddDbContext<HospitalDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("HospitalDb")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<HospitalDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["ProjectConfiguration:Jwt:Audience"],
                    ValidIssuer = Configuration["ProjectConfiguration:Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ProjectConfiguration:Jwt:Key"]))
                };
            });
            services.AddDistributedMemoryCache();

            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //   options.TokenValidationParameters = new TokenValidationParameters
            //   {
            //       ValidateIssuer = true,
            //       ValidateAudience = true,
            //       ValidateLifetime = true,
            //       ValidateIssuerSigningKey = true,
            //       ValidIssuer = Configuration["ProjectConfiguration:Jwt:Issuer"],
            //       ValidAudience = Configuration["ProjectConfiguration:Jwt:Audience"],
            //       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ProjectConfiguration:Jwt:Key"]))
            //   };
            //});

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAllergiesService, AllergiesService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IMedicalTreatmentService, MedicalTreatmentService>();
            services.AddScoped<ITherapyService, TherapyService>();
            services.AddScoped<IMedicamentTherapyService, MedicamentTherapyService>();
            services.AddScoped<IBloodUnitTherapyService, BloodUnitTherapyService>();
            services.AddScoped<IMedicamentService, MedicamentService>();
            services.AddScoped<IVacationRequestsService, VacationRequestsService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRelocationService, RelocationService>();
            services.AddHostedService<TimedHostedService>();
            services.AddScoped<IBloodUnitService, BloodUnitService>();
            services.AddScoped<IBloodAcquisitionService, BloodAcquisitionService>();
            services.AddScoped<IBloodExpenditureService, BloodExpenditureService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IApplicationPatientService, ApplicationPatientService>();
            services.AddScoped<IApplicationDoctorService, ApplicationDoctorService>();
            



            ProjectConfiguration config = new ProjectConfiguration();
            Configuration.Bind("EmailSettings", config.EmailSettings);
            Configuration.Bind("ProjectConfiguration", config);
            services.AddSingleton(config);
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
            }

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
