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
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://192.168.1.69:5173",
                "http://100.94.132.33:5173",
                "http://100.124.94.117:5173",
                "http://192.168.12.122:5173",
                "http://192.168.1.69",
                "http://100.94.132.33"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("LoginPolicy", context =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                SegmentsPerWindow = 2,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    options.AddPolicy("RegisterPolicy", context =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(10),
                SegmentsPerWindow = 2,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});

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
builder.Services.AddScoped<IUserDataInsightsByUserIdCase, UserDataInsightsByUserIdCase>();
builder.Services.AddScoped<IAMealPromptService, AIMealPromptService>();
builder.Services.AddScoped<IParseGeminiUserMealCase, ParseGeminiUserMealCase>();
builder.Services.AddScoped<IUserMealsCreate, UserMealsCreate>();
builder.Services.AddScoped<IGetUserMealsByDay, GetUserMealsByDay>();
builder.Services.AddScoped<IUserMealsDelete, UserMealDeleteCase>();
builder.Services.AddScoped<IGetUserMealsById, GetUserMealsById>();
builder.Services.AddScoped<IUserMealMetricsCase, UserMealMetricsCase>();
builder.Services.AddScoped<IRegisterUserBodyMetricsCase, RegisterUserBodyMetricsCase>();
builder.Services.AddScoped<IRegisterUserDataInsightsRulesCase, RegisterUserDataInsightsRulesCase>();
builder.Services.AddScoped<IGetUserMetricsCase, GetUserMetricsCase>();
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

builder.WebHost.UseUrls("http://0.0.0.0:5052");

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();