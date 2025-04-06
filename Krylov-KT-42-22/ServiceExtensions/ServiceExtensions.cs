using Krylov_KT_42_22.Interfaces.TeacherInterfaces;

namespace Krylov_KT_42_22.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            return services;
        }
    }
}
