using SeaBattle.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGameEndpoints();

app.UseDefaultFiles(); // 1-м
app.UseStaticFiles();  // 2-м

app.MapGet("/", ()=> Results.File("index.html", "text/html"));

app.Run();
