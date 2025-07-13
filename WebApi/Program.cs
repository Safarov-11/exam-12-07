using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Infrastructure.Data;
using Infrastructure.Seeds;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Cars.CarInterfaces;
using Infrastructure.Cars.CarRepositories;
using Infrastructure.Cars.CarServices;
using Infrastructure.Customers.CustomerInterfaces;
using Infrastructure.Customers.CustomerRepositories;
using Infrastructure.Customers.CustomerServices;
using Infrastructure.Branches.BrancheInterfaces;
using Infrastructure.Branches.BranchRepositories;
using Infrastructure.Branches.BrancheServices;
using Infrastructure.Rentals.RentalInterfaces;
using Infrastructure.Rentals.RentalRepositories;
using Infrastructure.Rentals.RentalServices;
using Infrastructure.Statistics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IBrancheRepository, BrancheRepository>();
builder.Services.AddScoped<IBrancheService, BrancheService>();

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services.AddScoped<IStatisticService, StatisticService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Описание вашего API",
        Contact = new OpenApiContact
        {
            Name = "Alijon Manonzoda",
            Email = "alijonzabirov20@gmail.com",
        }
    });
            
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите JWT токен: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
    {
        option.Password.RequireDigit = false;
        option.Password.RequireLowercase = false;
        option.Password.RequireUppercase = false;
        option.Password.RequireNonAlphanumeric = false;
        option.Password.RequiredLength = 4;
        option.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // = crm.omuz.tj
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


var scope = app.Services.CreateAsyncScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

await DefaultRoles.SeedRoleAsync(roleManager);
await DefaultUsers.SeedUserAsync(userManager);

var builder1 = WebApplication.CreateBuilder(args);


builder1.Logging.ClearProviders();
builder1.Logging.AddConsole();


// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStatusCodePages(); 
app.Run();
