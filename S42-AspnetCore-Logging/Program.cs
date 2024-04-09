using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();
//builder.Services.AddLogging(loggingBuilder =>
//{
//    loggingBuilder.AddConsole();
//    //loggingBuilder.AddSeq("http://localhost:5341", "yFe1aG6RfoUJpFxQIl8m");
//    //loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));

//    loggingBuilder.AddSerilog();

//});


builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
})
    .UseSerilog((context, configs) =>
    {
        configs.Enrich.WithThreadId();
        configs.Enrich.WithEnvironmentName();
        configs.WriteTo.Console();
        configs.WriteTo.Seq("http://localhost:5341", apiKey: "yFe1aG6RfoUJpFxQIl8m");
    });






// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
