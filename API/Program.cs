using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using vehicleDealer.Controllers;
using vehicleDealer.Core;
using vehicleDealer.Core.Interfaces;
using vehicleDealer.Core.Models;
using vehicleDealer.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "Vehicle Dealer API",
    Version = "v1",
    Description = "API for Vehicle Dealer Application",
    Contact = new OpenApiContact
    {
      Name = "Rian Negreiros Dos Santos",
      Email = "riannegreiros@gmail.com",
      Url = new Uri("https://github.com/RianNegreiros")
    }
  });

  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  c.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<PhotoSettings>(builder.Configuration.GetSection("PhotoSettings"));
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddTransient<IPhotoService, PhotoService>();
builder.Services.AddTransient<IPhotoStorage, FileSystemPhotoStorage>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
          options.Authority = "https://dev-lo6hbbeg443ymrdp.us.auth0.com/";
          options.Audience = "https://api.vehicle-dealer.com";
        });

builder.Services.AddAuthorization(options =>
{
  options.AddPolicy(Policies.RequireAdminRole, policy => policy.RequireClaim("https://api.vehicle-dealer.com/roles", "Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
