using FretWeb.Services;
using FretWeb.Utilities;

var builder = WebApplication.CreateBuilder(args);

// builder.AddHoneycombOpenTelemetry();

// Add services to the container.
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHealthChecks();

if (EnvironmentVariables.HaveValues("CLOUDFLARE_API_TOKEN", "CLOUDFLARE_API_ZONE"))
{
    builder.Services.AddHostedService<CloudflarePurgeService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const string staticFileCacheHeader = "public,max-age=604800";
        ctx.Context.Response.Headers.CacheControl = staticFileCacheHeader;
        ctx.Context.Response.Headers.Vary = "Accept-Encoding";
    },
});

app.UseRouting();

if (Environment.GetEnvironmentVariable("GOOGLE_ADS_TXT") is { Length: > 0 } googleAdsTxt)
{
    app.MapGet("/ads.txt", context =>
    {
        context.Response.ContentType = "text/text";
        context.Response.ContentLength = googleAdsTxt.Length;
        return context.Response.WriteAsync(googleAdsTxt);
    });
}

app.MapHealthChecks("/health");

Func<object, Task> setResponseHeaders = state =>
{
    const string cacheHeader = "public,max-age=86400";
    
    var headers = ((HttpContext)state).Response.Headers;
    headers.Add("X-Clacks-Overhead", "GNU Terry Pratchett");
    headers.CacheControl = cacheHeader;
    headers.Vary = "Accept-Encoding";
    return Task.CompletedTask;
};

app.Use(async (context, next) =>
{
    context.Response.OnStarting(setResponseHeaders, context);
    await next();
});

app.UseAuthorization();

// app.Use(ContentLengthMiddleware.SetContentLength);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
