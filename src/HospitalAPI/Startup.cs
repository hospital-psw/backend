using HospitalAPI.Configuration;
using HospitalAPI.EmailServices;
using HospitalAPI.Mappers;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.Blood;
using HospitalLibrary.Core.Service.Blood.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


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

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
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
            services.AddScoped<IBloodUnitService, BloodUnitService>();
            services.AddScoped<IBloodAcquisitionService, BloodAcquisitionService>();
            services.AddScoped<IBloodExpenditureService, BloodExpenditureService>();



            ProjectConfiguration config = new ProjectConfiguration();
            Configuration.Bind("EmailSettings", config.EmailSettings);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
