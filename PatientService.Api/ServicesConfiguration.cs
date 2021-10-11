using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PatientService.Data.ReposiotryEF;
using PatientService.Domain.Manager;
using PatientService.Domain.Repository;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PatientService.Api.Controllers;

namespace PatientService.Api
{
    public static class ServicesConfiguration
    {
        public static void Config(this IServiceCollection services, IConfiguration configuration)
        {

            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<PatientsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<PatientsDbContext, PatientsDbContext>();
            services.AddScoped<IManager, ManagerImpl>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Patients Service",
                    Version = "V1",
                    Description = "Patients CRUD Service"
                });
            });


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddMemoryCache();

            services.AddSingleton<ILogger>(provider =>
                provider.GetRequiredService<ILogger<PatientsController>>());

        }
    }
}
