﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceAuth_API;
using ServiceAuth_API.Services;
using ServiceAuth_API.Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configuraci�n de Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Dominio"],
            ValidAudience = builder.Configuration["JWT:appApi"],
            LifetimeValidator = TokenLifetimeValidator.Validate,
            IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Password"])
            )

        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyLocal",
    policy =>
    {
        policy.WithOrigins("http://localhost:5001",
                            "http://uneed.com",
                            "https://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });


    options.AddPolicy("PauloVi",
        policy =>
        {
            policy.WithOrigins("http://localhost:8081")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    options.AddPolicy("All",
        policy =>
        {
            policy.WithOrigins("*",
                "https://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });

});
//Active Services
builder.Services.AddScoped<MongoDBRepository>();
builder.Services.AddScoped<IServiceAuth, ServiceAuth>();
builder.Services.AddScoped<IServicePropietor, ServicePropietor>();
builder.Services.AddScoped<IServiceProperty, ServiceProperty>();
builder.Services.AddScoped<IServiceSupport, ServiceSupport>();
builder.Services.AddScoped<IServiceDocument, ServiceDocument>();
builder.Services.AddScoped<IServicePayment, ServicePayment>();
builder.Services.AddScoped<IServiceReservation, ServiceReservation>();
builder.Services.AddScoped<IServiceAnnouncement, ServiceAnnouncement>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
