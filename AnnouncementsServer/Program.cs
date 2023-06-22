using AnnouncementsServer.Filters;
using AnnouncementsServer.Models;
using AnnouncementsServer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AnnouncementsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers((options)=>
            {
                options.Filters.Add(typeof(ApplicationExceptionFilterAttribute));
            });
            string dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(dbConnection));
            builder.Services.AddScoped<AnnouncementsService>();
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnnouncementsWebApi", Version = "v1" });
                });
            }

            WebApplication app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnnouncementsWebApi v1"));
            }
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}