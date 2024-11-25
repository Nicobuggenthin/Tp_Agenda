using Agenda.Managers.Managers;
using Agenda.Managers.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// Registrar repositorios (utilizando Dapper)
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddScoped<IEventoRepository>(_ => new EventoRepository(connectionString));
builder.Services.AddScoped<IEventoManager, EventoManager>();

// Registrar IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configuraci�n de autenticaci�n con Google
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie();
//.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
//{
//    options.ClientId = builder.Configuration["GooglaKeys:ClientId"];
//    options.ClientSecret = builder.Configuration["GooglaKeys:ClientPriv"];
//    options.Events.OnCreatingTicket = ctx =>
//    {
        // Aqu� se puede hacer la creaci�n de usuario si es necesario.
//        return Task.CompletedTask;
//    };
//});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
