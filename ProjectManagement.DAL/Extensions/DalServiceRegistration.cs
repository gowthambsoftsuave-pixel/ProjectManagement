using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.DAL.Data;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.IRepositories;
using ProjectManagement.DAL.Repositories;

namespace ProjectManagement.DAL.Extensions;

public static class DalServiceRegistration
{
    public static IServiceCollection AddDal(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<ProjectManagementDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IProjectTeamRepository, ProjectTeamRepository>();

        return services;
    }
}
