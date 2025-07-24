
using ContactFormApi.Data;
using ContactFormApi.DTOs;
using ContactFormApi.Repositories;
using ContactFormApi.Services;
using Microsoft.EntityFrameworkCore;

namespace ContactFormApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
                                    
                                    
            });

            //Add Db services also
            builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.
                            GetConnectionString("DefaultConnection")));

            //register repository
            builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();

            builder.Services.AddScoped<IContactMessageservice, ContactMessageservice>();
            //routiner

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapGet("/test/ContactForm", () => "Contact Form Api running!");

            app.MapPost("/api/contact", async (
                ContactMessageDto dto,
                IContactMessageservice service) =>
            {
                var (success, message) = await service.ProcessContactFormAsync(dto);

                return success
                        ? Results.Ok(new { success = true, message })
                        : Results.BadRequest(new { success = false, message });
            });

            app.MapGet("/api/all-contact", async (IContactMessageRepository repository) =>
            {
                var messages = await repository.GetAllMessagesAsync();
                return Results.Ok(messages);
            });

            app.MapControllers();

            app.Run();
        }
    }
}
