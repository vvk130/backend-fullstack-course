using Hangfire;
using Hangfire.PostgreSql;
using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddValidatorsFromAssemblyContaining<FileUploadRequestDtoValidator>();

builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IHorseService, HorseService>();
builder.Services.AddScoped<IHorseBreedService, HorseBreedService>();
builder.Services.AddScoped<IFoalCreationService, FoalCreationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard();
app.UseHangfireServer();
app.UseSwagger();
app.UseSwaggerUI();

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
