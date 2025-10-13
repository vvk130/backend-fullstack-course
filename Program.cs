using Hangfire;
using Hangfire.PostgreSql;
using CloudinaryDotNet;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection; 

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 12;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddApiEndpoints();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(12); 
    options.SlidingExpiration = true;                
});

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

builder.Services.AddAutoMapper(typeof(MappingProfile)); 

var app = builder.Build();

// app.UseHangfireDashboard();
// app.UseHangfireServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<ApplicationUser>();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
    string[] roles = [ "Admin", "User" ];

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
});


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
