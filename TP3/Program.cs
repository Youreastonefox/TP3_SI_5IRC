using TP3.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using TP3.Models.DataManager;
using TP3.Models.Repository;

namespace TP3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services
                .AddControllersWithViews()
                .AddNewtonsoftJson();
            builder.Services.AddDbContext<NotationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("NotationDbContext"))
            );

            builder.Services.AddDbContext<NotationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("NotationDbContext")));

            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

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
    }
}