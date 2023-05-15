using FretWeb.Telemetry;
using FretWeb.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
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

app.Use(async (context, next) =>
{
    if (context.Request.Query.TryGetValue("theme", out var values))
    {
        if (Enum.TryParse(values.FirstOrDefault(), true, out Theme theme))
        {
            context.Response.Cookies.Append("fretweb.theme", theme.ToString().ToLower());
        }

        var path = context.Request.Path;
        if (context.Request.Query.Count == 1)
        {
            context.Response.Redirect(path);
        }
        else
        {
            var query = context.Request.QueryString.ToString().Replace($"&theme={theme.ToString().ToLower()}", "");
            context.Response.Redirect($"{path}{query}");
        }

        return;
    }
    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();