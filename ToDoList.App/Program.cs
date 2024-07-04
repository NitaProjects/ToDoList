// Este archivo configura y arranca la aplicación Blazor WebAssembly.
// Se configura el contenedor de servicios, se añaden servicios esenciales como HttpClient y LocalStorageAccessor,
// y se especifican los componentes raíz de la aplicación. Luego, se construye y se ejecuta la aplicación.

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoList.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Añade el componente raíz App al elemento con ID "app" en el HTML.
builder.RootComponents.Add<App>("#app");

// Añade el componente HeadOutlet al head del documento, justo después de los elementos existentes.
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configura el servicio HttpClient para que esté disponible para inyección de dependencias en toda la aplicación.
// El HttpClient se configura con la dirección base del entorno de alojamiento.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Añade el servicio LocalStorageAccessor para que esté disponible para inyección de dependencias en toda la aplicación.
builder.Services.AddScoped<LocalStorageAccessor>();

// Construye y ejecuta la aplicación.
await builder.Build().RunAsync();
