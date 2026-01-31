using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.BLL.Interface;
using ProjectManagement.BLL.Service;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Extensions;
using ProjectManagement.BLL.Mapping;

namespace ProjectManagement.BLL.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDal(config);
        
        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAuthService, AuthService>();



        return services;
    }
}
