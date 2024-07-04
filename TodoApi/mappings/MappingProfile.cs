// Este archivo define una clase para acceder al almacenamiento local del navegador desde una aplicación Blazor.
// La clase `LocalStorageAccessor` utiliza JavaScript Interop (JSInterop) para interactuar con el almacenamiento local.
// Proporciona métodos para obtener, establecer, eliminar y limpiar valores en el almacenamiento local.
// La clase también maneja la inicialización y eliminación de referencias a objetos JavaScript.

using Microsoft.JSInterop;
using System.Text.Json;

namespace ToDoList.App
{
    public class LocalStorageAccessor : IAsyncDisposable
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new(); // Referencia perezosa al objeto JavaScript.
        private readonly IJSRuntime _jsRuntime; // Referencia al tiempo de ejecución JSInterop.

        public LocalStorageAccessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime; // Inicializa el tiempo de ejecución JSInterop.
        }

        // Obtiene un valor del almacenamiento local y lo deserializa al tipo especificado.
        public async Task<T> GetValueAsync<T>(string key)
        {
            await WaitForReference(); // Espera a que la referencia al objeto JS esté disponible.
            var jsonResult = await _accessorJsRef.Value.InvokeAsync<string>("get", key); // Llama a la función 'get' en el objeto JS.
            var result = JsonSerializer.Deserialize<T>(jsonResult); // Deserializa el resultado JSON al tipo especificado.
            return result;
        }

        // Establece un valor en el almacenamiento local después de serializarlo a JSON.
        public async Task SetValueAsync<T>(string key, T value)
        {
            await WaitForReference(); // Espera a que la referencia al objeto JS esté disponible.
            string jsonValue = JsonSerializer.Serialize(value); // Serializa el valor al formato JSON.
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, jsonValue); // Llama a la función 'set' en el objeto JS.
        }

        // Elimina un valor del almacenamiento local.
        public async Task RemoveValueAsync(string key)
        {
            await WaitForReference(); // Espera a que la referencia al objeto JS esté disponible.
            await _accessorJsRef.Value.InvokeVoidAsync("remove", key); // Llama a la función 'remove' en el objeto JS.
        }

        // Limpia todos los valores del almacenamiento local.
        public async Task ClearAsync()
        {
            await WaitForReference(); // Espera a que la referencia al objeto JS esté disponible.
            await _accessorJsRef.Value.InvokeVoidAsync("clear"); // Llama a la función 'clear' en el objeto JS.
        }

        // Espera a que la referencia al objeto JS esté disponible, inicializándola si es necesario.
        private async Task WaitForReference()
        {
            if (!_accessorJsRef.IsValueCreated) // Verifica si la referencia al objeto JS ya está creada.
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>
                ("import", "/js/LocalStorageAccessor.js")); // Importa el módulo JS y asigna la referencia.
            }
        }

        // Elimina la referencia al objeto JS cuando la instancia se desecha.
        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated) // Verifica si la referencia al objeto JS ya está creada.
            {
                await _accessorJsRef.Value.DisposeAsync(); // Elimina la referencia al objeto JS.
            }
        }
    }
}

