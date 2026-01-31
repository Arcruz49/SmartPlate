using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SmartPlate.Application.Interfaces;
using SmartPlate.Application.UseCases;
using SmartPlate.Application.Security;
using SmartPlate.Infrastructure.Data;
using SmartPlate.Infrastructure.AI;
using System.Text.Json.Serialization;
using Npgsql;
using SmartPlate.Domain.Enums;
using SmartPlate.Infrastructure.AI.Gemini;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

dataSourceBuilder.MapEnum<BiologicalSex>("biological_sex");
dataSourceBuilder.MapEnum<TrainingType>("training_type");
dataSourceBuilder.MapEnum<TrainingIntensity>("training_intensity");
dataSourceBuilder.MapEnum<DailyActivityLevel>("daily_activity_level");
dataSourceBuilder.MapEnum<UserGoals>("user_goal");

var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(dataSource));

var key = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? ""))
    };
});

builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IUserDataCreateCase, UserDataCreateCase>();
builder.Services.AddScoped<IUserDataByUserIdCase, UserDataByUserIdCase>();
builder.Services.AddScoped<IAIInsightsPromptService, AIInsightsPromptService>();
builder.Services.AddScoped<IUserDataInsightsCreateCase, UserDataInsightsCreateCase>();
builder.Services.AddScoped<IParseGeminiUserInsightsCase, ParseGeminiUserInsightsCase>();
builder.Services.AddHttpClient<IAIClient, AIClient>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection("AI:Gemini")
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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