﻿using FluentValidation.AspNetCore;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using School.Domain.DTOs.Student;
using Shop.Infra.Data.Context;
using Shop.Infra.IoC;
using System.ComponentModel.DataAnnotations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region dbContext

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        );
});

#endregion



builder.Services.AddControllers()
    .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<RegisterStudentDto>())
    .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<LoginStudentDTO>())
    .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<ActiveAccountDTO>())
    .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<ResetPasswordDTO>());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region IoC
static void RegisterServices(IServiceCollection services) => DepenencyContainer.RegisterServices(services);

RegisterServices(builder.Services);
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
#endregion

#region token
//var secretKey = builder.Configuration.GetValue<string>("TokenKey");
//var tokenTimeOut = builder.Configuration.GetValue<int>("TokenTimeOut");

//var key = Encoding.UTF8.GetBytes(secretKey);

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        //برای کنترل زمان توکن
//        ClockSkew = TimeSpan.FromMinutes(tokenTimeOut),
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});
#endregion

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Authentication:Issuer"],
//            ValidAudience = builder.Configuration["Authentication:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
//        };
//    }
//    );
#region mizfa
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
             .AddJwtBearer(options =>
             {
                 var secretkey = Encoding.UTF8.GetBytes(builder.Configuration["SiteSettings:SecretKey"]);
                 var encryptionkey = Encoding.UTF8.GetBytes(builder.Configuration["SiteSettings:EncrypKey"]);

                 var validationParameters = new TokenValidationParameters
                 {
                     RequireSignedTokens = true,

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                     RequireExpirationTime = true,
                     ValidateLifetime = true,

                     ValidateAudience = true, //default : false
                     ValidAudience = builder.Configuration["SiteSettings:Audience"],

                     ValidateIssuer = true, //default : false
                     ValidIssuer = builder.Configuration["SiteSettings:Issuer"],

                     TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                 };

                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = validationParameters;
             });
#endregion





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();
app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
