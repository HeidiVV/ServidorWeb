using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using SharpCompress.Compressors.PPMd;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.PropertyNamingPolicy = null);

    builder.Services.AddCors();

var app = builder.Build();

app.UseCors(Policy => Policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("/", () => "Hello World!");

app.MapPost("/acceso/ingresar", RegistroRequestHandler.Ingresar);

app.MapPost("/recuperar/password", RegistroRequestHandler.Aceptar);

app.MapPost("/datos-registro/crear-cuenta", RegistroRequestHandler.CrearCuenta);

app.MapPost("/crear-categoria/crear", CategoriasRequestHandler.Crear);

app.MapGet("/listar-categorias/listar",CategoriasRequestHandler.Listar);

app.MapPost("/lenguajes", LenguajeRequestHandler.CrearRegistro);

app.MapGet("/lenguaje/{idCategoria}", LenguajeRequestHandler.ListarRegistros);

app.MapDelete("/lenguajes/{id}", LenguajeRequestHandler.Eliminar);

app.MapGet("/lenguaje/buscar", LenguajeRequestHandler.Buscar);

app.Run();