using Hangfire;
using Hangfire.PostgreSql;
using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRabbitMQ", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

        policy.WithOrigins("https://frontend-fullstck.netlify.app")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 12;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton(provider =>
{
    var url = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

    if (string.IsNullOrWhiteSpace(url))
        throw new InvalidOperationException("CLOUDINARY_URL is not configured.");

    var uri = new Uri(url.Replace("cloudinary://", "https://"));
    var userInfo = uri.UserInfo.Split(':');

    if (userInfo.Length != 2)
        throw new InvalidOperationException("CLOUDINARY_URL is malformed.");

    var account = new Account(
        cloud: uri.Host,         
        apiKey: userInfo[0],     
        apiSecret: userInfo[1]   
    );

    return new Cloudinary(account);
});

var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
var vhost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST");
var username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ItemCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(host, vhost, h =>
        {
            h.Username(username);
            h.Password(password);
        });

        cfg.ReceiveEndpoint("item-created-event", e =>
        {
            e.ConfigureConsumer<ItemCreatedConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers()
    .AddFluentValidation()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddScoped<IHorseService, HorseService>();
builder.Services.AddScoped<IHorseBreedService, HorseBreedService>();
builder.Services.AddScoped<IFoalCreationService, FoalCreationService>();
builder.Services.AddScoped<IPuzzleService, PuzzleService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
builder.Services.AddScoped<ICompStatisticsService, CompStatisticsService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile)); 

var app = builder.Build();

app.UseCors("AllowRabbitMQ");

app.MapIdentityApi<ApplicationUser>();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok("Logged out");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" || context.Request.Path == "/index")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

app.MapControllers();

app.Run();
