using AutoMapper;
using HospitalAPI.Configuration;
using HospitalAPI.Controllers.TenderStatistics;
using HospitalAPI.Dto.AppUsers;
using HospitalAPI.EmailServices;
using HospitalAPI.Mappers;
using HospitalAPI.TokenServices;
using HospitalLibrary.Core.Model.ApplicationUser;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.AppUsers;
using HospitalLibrary.Core.Service.AppUsers.Core;
using HospitalLibrary.Core.Service.Blood;
using HospitalLibrary.Core.Service.Blood.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Core.Service.Examinations;
using HospitalLibrary.Core.Service.Examinations.Core;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            options.UseSqlServer(Configuration.GetConnectionString("HospitalDb"),
            providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<HospitalDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            });

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

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = 401;
            //        return Task.CompletedTask;
            //    };
            //});

            services.AddDistributedMemoryCache();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
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
            services.AddScoped<IRenovationService, RenovationService>();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddScoped<IRoomScheduleService, RoomScheduleService>();
            services.AddScoped<IConsiliumService, ConsiliumService>();
            services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<IAnamnesisService, AnamnesisService>();
            services.AddScoped<IRenovationEventService, RenovationEventService>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<IExaminationEventService, ExaminationEventService>();
            services.AddScoped<IBloodAdditionService, BloodAdditionService>();
            services.AddScoped<IAppointmentSchedulingService, AppointmentSchedulingService>();
            services.AddScoped<IExaminationStatisticsService, ExaminationStatisticsService>();

            services.AddScoped<IConnections, Connections>();


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
