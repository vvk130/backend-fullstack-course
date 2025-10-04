DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IHorseService, HorseService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
