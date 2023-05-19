using FretWeb.Telemetry;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.AddHoneycombOpenTelemetry();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

const int duration = 60 * 60;
var cacheHeader = $"public,max-age={duration}";
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => { ctx.Context.Response.Headers[HeaderNames.CacheControl] = cacheHeader; }
});

app.UseRouting();

app.Use(async (context, next) =>
{
    context.Response.Headers.CacheControl = cacheHeader;
    context.Response.Headers.Vary = "Accept-Encoding";
    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
