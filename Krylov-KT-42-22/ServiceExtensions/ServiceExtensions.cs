using Krylov_KT_42_22.Interfaces.TeacherInterfaces;
using Krylov_KT_42_22.Interfaces.DepartmentInterfaces;
using Krylov_KT_42_22.Interfaces.LoadInterfaces;
using System.Runtime.CompilerServices;

namespace Krylov_KT_42_22.ServiceExtensions
{
    public  static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IDepartmentServices, DepartmentService>();
            services.AddScoped<ILoadService, LoadService>();
            return services;
        }

    }
}
