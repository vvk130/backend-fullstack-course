using Hangfire;
using Hangfire.PostgreSql;
using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRabbitMQ", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            // ONLY TESTING
    });
});


builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddValidatorsFromAssemblyContaining<FileUploadRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<HorseBreedValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LevelValidator>();

// builder.Services.AddHangfire(config =>
// {
//     config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Default"));
// });
// builder.Services.AddHangfireServer();

builder.Services.AddScoped<IHorseService, HorseService>();
builder.Services.AddScoped<IHorseBreedService, HorseBreedService>();
builder.Services.AddScoped<IFoalCreationService, FoalCreationService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.UseHangfireDashboard();
// app.UseHangfireServer();
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
