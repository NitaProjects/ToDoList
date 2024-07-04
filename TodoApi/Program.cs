// Este archivo configura y arranca la aplicación web de la API de lista de tareas.
// Se configura el contenedor de servicios, se añaden servicios esenciales como CORS, controladores, contexto de base de datos,
// Swagger y AutoMapper. Luego, se construye y se ejecuta la aplicación, configurando el pipeline de solicitudes HTTP.

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using ToDoList.DTO;
using AutoMapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor.
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy =>
    policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin())); // Configura CORS para permitir cualquier encabezado, método y origen.
builder.Services.AddControllers(); // Añade soporte para controladores de API.
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList")); // Configura el contexto de la base de datos para usar una base de datos en memoria.
builder.Services.AddEndpointsApiExplorer(); // Añade soporte para la exploración de endpoints de API.
builder.Services.AddSwaggerGen(); // Añade y configura Swagger para la generación de documentación de la API.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Añade y configura AutoMapper, escaneando el ensamblado actual en busca de perfiles de mapeo.

var app = builder.Build(); // Construye la aplicación.


// Configura el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita Swagger en el entorno de desarrollo.
    app.UseSwaggerUI(); // Habilita la interfaz de usuario de Swagger en el entorno de desarrollo.
}

app.UseHttpsRedirection(); // Habilita la redirección HTTPS.

app.UseCors(); // Habilita CORS con la configuración predeterminada.

app.UseAuthorization(); // Habilita la autorización.

app.MapControllers(); // Mapea los controladores a las rutas de la API.

app.Run(); // Ejecuta la aplicación.
