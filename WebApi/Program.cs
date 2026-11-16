using LogicaAccesoDato.EF;
using LogicaAccesoDato.EF.Repositorios;
using LogicaDeAplicacion.CasosDeUso.Categorias;
using LogicaDeAplicacion.CasosDeUso.Historia;
using LogicaDeAplicacion.CasosDeUso.Usuario;
using LogicaDeAplicacion.CasosDeUso.Usuarios;
using Swashbuckle.AspNetCore;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Categorias;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaDeAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.OpenApi;
using ObligatorioDW.InterfacesDeRepositorio;
using Swashbuckle.AspNetCore.Filters;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using ObligatorioDW.InterfacesDelRepositorio;
using LogicaAccesoDato.EF.Repositorios;

using LogicaDeAplicacion.InterfacesDeCasoDeUso.Lecturas;
using LogicaDeAplicacion.CasosDeUso.Lecturas;

using WebApi.Servicios;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(
    opciones =>
    {
        opciones.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        {
            Description = "Autorizacion estandar mediante esquema Bearer",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        opciones.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
//Inicializamos Repositorios
builder.Services.AddScoped<ObligatorioContext>();

builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioHistoria, RepositorioHistoria>();
builder.Services.AddScoped<IRepositorioLectura, RepositorioLectura>();

//Inicializamos Casos de Uso
builder.Services.AddScoped<IObtenerTodas, ObtenerTodasCU>();
builder.Services.AddScoped<IAgregarUsuario, AgregarUsuarioCU>();

builder.Services.AddScoped<IListarUsuarios, ListarUsuariosCU>();


builder.Services.AddScoped<ILoginUsuario, LoginCU>();
builder.Services.AddScoped<IObtenerTodasHistorias, ObtenerTodasHistoriasCU>();
builder.Services.AddScoped<IAgregarHistoria, AgregarHistoriaCU>();
builder.Services.AddScoped<IEditarHistoria, EditarHistoriaCU>();

builder.Services.AddScoped<IObtenerHistorialLecturas, ObtenerHistorialLecturasCU>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var jwtKey = builder.Configuration["Jwt:Key"]
             ?? throw new Exception("Falta configurar Jwt:Key");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.IncludeErrorDetails = true;
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
            
            
        };
        opciones.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = contexto =>
            {
                Console.WriteLine("ERROR JWT: " + contexto.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddScoped<IIniciarLectura, IniciarLecturaCU>();

builder.Services.AddScoped<IFinalizarLectura, FinalizarLecturaCU>();




builder.Services.AddScoped<IObtenerRankingHistorias, ObtenerRankingHistoriasCU>();


builder.Services.AddScoped<GeneradorJwt>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
