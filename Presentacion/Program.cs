using LogicaAccesoDato.EF;
using LogicaAccesoDato.EF.Repositorios;
using LogicaDeAplicacion.CasosDeUso.Categorias;
using LogicaDeAplicacion.CasosDeUso.Historia;
using LogicaDeAplicacion.CasosDeUso.Usuario;
using LogicaDeAplicacion.CasosDeUso.Usuarios;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Categorias;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
using ObligatorioDW.InterfacesDeRepositorio;
using LogicaDeAplicacion.CasosDeUso.Capitulos;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using LogicaDeAplicacion.CasosDeUso.Lecturas;
using ObligatorioDW.InterfacesDelRepositorio;
using LogicaAccesoDato.EF.Repositorios;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
//Inicializamos Repositorios
builder.Services.AddScoped<ObligatorioContext>();

builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioHistoria, RepositorioHistoria>();
builder.Services.AddScoped<IRepositorioCapitulo, RepositorioCapitulo>();
builder.Services.AddScoped<IRepositorioLectura, RepositorioLectura>();

//Inicializamos Casos de Uso
builder.Services.AddScoped<IObtenerTodas, ObtenerTodasCU>();
builder.Services.AddScoped<IAgregarUsuario, AgregarUsuarioCU>();

builder.Services.AddScoped<IListarUsuarios, ListarUsuariosCU>();


builder.Services.AddScoped<ILoginUsuario, LoginCU>();
builder.Services.AddScoped<IObtenerTodasHistorias, ObtenerTodasHistoriasCU>();
builder.Services.AddScoped<IAgregarHistoria, AgregarHistoriaCU>();
builder.Services.AddScoped<IEditarHistoria, EditarHistoriaCU>();
builder.Services.AddScoped<IEliminarHistoria, EliminarHistoriaCU>();

builder.Services.AddScoped<IAgregarCapitulo, AgregarCapituloCU>();

builder.Services.AddScoped<IObtenerRankingHistorias, ObtenerRankingHistoriasCU>();

builder.Services.AddScoped<IObtenerFinalesMasAlcanzados, ObtenerFinalesMasAlcanzadosCU>();



builder.Services.AddScoped<IObtenerRankingLectores, ObtenerRankingLectoresCU>();
builder.Services.AddScoped<ServicioAuditoria>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession(); //permitre usar las sesiones mientras los usuarios navegan por la aplicacion

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
