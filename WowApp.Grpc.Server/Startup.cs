using Microsoft.EntityFrameworkCore;
using WowApp.Database;
using WowApp.Database.User;
using WowApp.Grpc.Services;

namespace WowApp.Grpc;

public class Startup
{
    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        var typeOfContent = typeof(Startup);

        services.AddDbContext<PostgresContext>(
            options => options.UseNpgsql(
                Configuration.GetConnectionString("WowDB"), b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
            )
        );

        services.AddGrpc();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDatabaseContainer, DatabaseContainer>();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostgresContext dbContext)
    {
        if (env.IsDevelopment()) { }

        app.UseRouting();
        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapGrpcService<UserService>();
            });
    }
}