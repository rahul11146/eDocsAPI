using Polly;
using Polly.CircuitBreaker;
using API.Connections;
using eDocsAPI.Data;
using Polly.Bulkhead;
using eDocsAPI.Interface;
using eDocsAPI.Repository;



namespace CircuitBreaker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddTransient<SQLDbContext, SQLDbContext>();
        builder.Services.AddTransient<IProject, ProjectRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureService(builder.Services, builder.Configuration);

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    public static void ConfigureService(IServiceCollection services,IConfiguration configuration)
    {
        services.AddHttpClient<IJokeService, JokeService>(client =>
        {
            client.BaseAddress = new Uri("https://official-joke-api.appspot.com/random_joke");
        }).AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(3, TimeSpan.FromMilliseconds(120000)));

        
    }
}

