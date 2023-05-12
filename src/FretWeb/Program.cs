using FretWeb.Telemetry;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.AddHoneycombOpenTelemetry();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

const int duration = 60 * 60 * 24;
var cacheHeader = $"public,max-age={duration}";
// app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => { ctx.Context.Response.Headers[HeaderNames.CacheControl] = cacheHeader; }
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();