using frontend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure HttpClient
builder.Services.AddHttpClient("ventasApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7099/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    // Permitir acceso a login y logout sin autenticación
    if (path.Value.Contains("/Auth/Login") ||
        path.Value.Contains("/Auth/Logout") ||
        path.Value.Contains("/Error"))
    {
        await next();
        return;
    }

    // Verificar si el usuario está autenticado
    if (string.IsNullOrEmpty(context.Session.GetString("Usuario")))
    {
        context.Response.Redirect("/Auth/Login");
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();