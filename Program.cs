using WebAppBach.Components;
using WebAppBach.Components.Services;
using WebAppBach.Components.Services.Interfaces;
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


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
