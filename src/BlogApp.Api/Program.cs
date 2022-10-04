using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Commons.Middlewares;
using BlogApp.Api.Commons.Security;
using BlogApp.Api.Data;
using BlogApp.Api.Inerfaces.Managers;
using BlogApp.Api.Inerfaces.Repositories;
using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.Repositrories;
using BlogApp.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
var connectionString = builder.Configuration.GetConnectionString("PostgresManagement");
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseNpgsql(connectionString));

//Repositories
builder.Services.AddScoped<IBlogAppRepository, BlogAppRepository>();
builder.Services.AddScoped<IUserRepositroy, UserRepositrory>();


//Service
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<ExceptionHandlerMiddlewar>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
