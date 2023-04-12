
using Microsoft.Extensions.DependencyInjection;
using School.Application.Interaces;
using School.Application.Services;
using School.Domain.Interaces;
using School.Infra.Data.Repositories;

namespace Shop.Infra.IoC
{
    public class DepenencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<ISiteService, SiteService>();
            #endregion
            #region Repositories
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICollegeRepository, CollegeRepository>();
            services.AddScoped<ICollegeGalleryRepository, CollegeGalleryRepository>();
            #endregion

            #region Tools
            services.AddScoped<IPasswordHelper, MD5PasswordHelper>();
            services.AddScoped<ISmsService, SmsService>();
            #endregion

        }
    }
}
