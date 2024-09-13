using DataAccess.Context;
using DataAccess.DAOs;
using KiotVietServices.Services;
using CampusEatsAPI.Services;
using VNPAYServices.Config;
using Microsoft.EntityFrameworkCore;
using KiotVietServices.Config;
using CampusEatsLibrary.Services;
using NhanhVNServices.Repository;
using NhanhVNServices.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CampusEatsAPI")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IKiotVietRepository, KiotVietRepository>();
builder.Services.AddTransient<INhanhVNRepository, NhanhVNRepository>();
builder.Services.AddTransient<PaymentDAO>();
builder.Services.AddTransient<OrderDAO>();
builder.Services.AddTransient<CartDAO>();
builder.Services.AddTransient<MenuDAO>();
builder.Services.AddTransient<AccountDAO>();
builder.Services.AddTransient<ApiClient>();
builder.Services.AddSingleton<AccessTokenManager>();

builder.Services.Configure<VnpayConfig>(
    builder.Configuration.GetSection(VnpayConfig.ConfigName));
builder.Services.Configure<KiotVietConfig>(
    builder.Configuration.GetSection(KiotVietConfig.ConfigName));
builder.Services.Configure<NhanhVNConfig>(
    builder.Configuration.GetSection(NhanhVNConfig.ConfigName));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "https://campuseats-three.vercel.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
