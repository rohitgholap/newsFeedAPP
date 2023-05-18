using Microsoft.EntityFrameworkCore;
using NewsFeedApp.Models;
using NewsFeedApp.Models.Repository;
using NewsFeedApp.Services;
using Microsoft.Extensions.Logging;

namespace NewsFeedApp
{
    public class Program
    {
        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.            
            builder.Services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("NewsFeedAppLogger"));


            builder.Services.AddControllers();
            builder.Services.AddDbContext<NewsFeedContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NewsFeedConnectionString"));
            });
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<INewsFeedServices, NewsFeedServices>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}