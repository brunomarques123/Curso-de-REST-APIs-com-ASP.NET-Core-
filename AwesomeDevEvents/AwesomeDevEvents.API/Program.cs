using AwesomeDevEvents.API.Mappers;
using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AwesomeDevEvents.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Conectar com banco
            var connectionString = builder.Configuration.GetConnectionString("DevEventsCs");


            // Add services to the container.
            //builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseInMemoryDatabase("DevEventDb"));
            builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(typeof(DevEventProfile).Assembly);

            builder.Services.AddControllers();

            // Configuração de CORS - Permitir todas as origens, métodos e cabeçalhos
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Swagger configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AwesomeDevEvents API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Bruno Marques",
                        Email = "b_marques85@live.com",
                        Url = new Uri("https://www.linkedin.com/in/bruno-marques-327523119/")
                    }
                });

                var xmlFile = "AwesomeDevEvents.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Swagger configuration
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AwesomeDevEvents API v1");
                    c.RoutePrefix = string.Empty; // Set Swagger as the default page
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Adicionar o middleware de CORS
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map API controllers
            });

            app.Run();
        }
    }
}
