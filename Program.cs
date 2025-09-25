using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WebAppBach.Components;
using WebAppBach.Components.Services;
using WebAppBach.Components.Services.Interfaces;
using WebAppBach.Components.Util;
using WebAppBach.Data;
using WebAppBach.Repository;
using WebAppBach.Repository.Interfaces;


//En Blazor .NET 8, como en versiones anteriores, se pueden registrar servicios
//en tres niveles:

//Transitorio(Transient): Una nueva instancia del servicio es creada cada vez que
//se requiere.
//De Ámbito (Scoped): Se crea una instancia por cada solicitud (en Blazor Server es
//por cada conexión, y en Blazor WebAssembly es por la vida útil de la sesión).
//Singleton: Se crea una única instancia para toda la aplicación.

var builder = WebApplication.CreateBuilder(args);


/* Connection to DB using Identity */
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()  O AddDefaultIdentity<ApplicationUser> como se muestra arriba
//.AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Construcción de servicio singleton
builder.Services.AddSingleton<IMyServices, MyService>();

builder.Services.AddSingleton<IElementoStateService, ElementoStateService>();
 

// Registro de servicio Scoped
// builder.Services.AddSingleton<IOtherService, MyOtherService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

// Identity
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


//Identity auth
//app.MapPost("/login", async(HttpContext httpContext, string email, string password, [FromServices] SignInManager<ApplicationUser> signInManager)=>
//{
//    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//    {
//        return Results.BadRequest("Email y contraseña son requeridos.");
//    }

//    var result = await signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

//    if (result.Succeeded)
//    {
//        return Results.LocalRedirect("/");
//    }

//    // Si falla, redirige de vuelta al login (ejemplo ruta)
//    return Results.LocalRedirect("/login?error=Credenciales+inválidas");
//});


app.MapPost("/logout", async (HttpContext httpContext, [FromServices] SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("/");
});


using (var scope = app.Services.CreateScope())
{
    await DataSeeder.SeedRolesAsync(scope.ServiceProvider);
}


app.Run();
