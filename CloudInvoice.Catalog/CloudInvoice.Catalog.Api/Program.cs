
using CloudInvoice.Catalog.Api.Middlewares;
using CloudInvoice.Catalog.Application.Interfaces;
using CloudInvoice.Catalog.Application.Services;
using CloudInvoice.Catalog.Domain.Interfaces;
using CloudInvoice.Catalog.Infrastructure.Data;
using CloudInvoice.Catalog.Infrastructure.Repositories;
using CloudInvoice.Catalog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CloudInvoice.Catalog.Api
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
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Cola aqui o token JWT (sem a palavra 'Bearer', o Swagger já a adiciona)."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ProductService>();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"]
                ?? throw new InvalidOperationException("JwtSettings:Secret não configurado no appsettings.json");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
