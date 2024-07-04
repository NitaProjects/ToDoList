using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using ToDoList.DTO;
using AutoMapper;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")] // Define la ruta base para las solicitudes a este controlador.
    [ApiController] // Indica que este controlador responderá a solicitudes HTTP y facilitará la validación automática del modelo.
    public class ListsController : ControllerBase
    {
        private readonly TodoContext _context; // Contexto de la base de datos para interactuar con las entidades.
        private readonly IMapper _mapper; // Mapper para convertir entre modelos y DTOs.

        public ListsController(TodoContext context, IMapper mapper)
        {
            _context = context; // Inicializa el contexto de la base de datos.
            _mapper = mapper; // Inicializa el mapper.
        }

        // Método GET para obtener todas las listas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListDto>>> GetLists()
        {
            var lists = await _context.Lists
                .Include(l => l.SubLists) // Incluye sublistas en la consulta.
                .Include(l => l.Tasks) // Incluye tareas en la consulta.
                .ToListAsync(); // Ejecuta la consulta de manera asincrónica.
            return Ok(_mapper.Map<IEnumerable<ListDto>>(lists)); // Mapea las listas a DTOs y las devuelve en la respuesta.
        }

        // Método GET para obtener una lista por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ListDto>> GetList(int id)
        {
            var list = await _context.Lists
                .Include(l => l.SubLists) // Incluye sublistas en la consulta.
                .Include(l => l.Tasks) // Incluye tareas en la consulta.
                .FirstOrDefaultAsync(l => l.ListId == id); // Busca la lista por su ID.

            if (list == null)
            {
                return NotFound(); // Devuelve un 404 si no se encuentra la lista.
            }

            return Ok(_mapper.Map<ListDto>(list)); // Mapea la lista a un DTO y la devuelve en la respuesta.
        }

        // Método PUT para actualizar una lista existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, ListDto listDto)
        {
            if (id != listDto.ListId)
            {
                return BadRequest(); // Devuelve un 400 si los IDs no coinciden.
            }

            var list = _mapper.Map<List>(listDto); // Mapea el DTO a un modelo.
            _context.Entry(list).State = EntityState.Modified; // Marca la lista como modificada.

            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios de manera asincrónica.
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
                {
                    return NotFound(); // Devuelve un 404 si la lista no existe.
                }
                else
                {
                    throw; // Relanza la excepción si ocurre algún otro error.
                }
            }

            return NoContent(); // Devuelve un 204 si la actualización fue exitosa.
        }

        // Método POST para crear una nueva lista
        [HttpPost]
        public async Task<ActionResult<ListDto>> PostList(ListDto listDto)
        {
            var list = _mapper.Map<List>(listDto); // Mapea el DTO a un modelo.
            _context.Lists.Add(list); // Añade la nueva lista al contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios de manera asincrónica.

            return CreatedAtAction(nameof(GetList), new { id = list.ListId }, _mapper.Map<ListDto>(list)); // Devuelve un 201 con la nueva lista.
        }

        // Método DELETE para eliminar una lista por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id); // Busca la lista por su ID.
            if (list == null)
            {
                return NotFound(); // Devuelve un 404 si no se encuentra la lista.
            }

            _context.Lists.Remove(list); // Elimina la lista del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios de manera asincrónica.

            return NoContent(); // Devuelve un 204 si la eliminación fue exitosa.
        }

        // Método privado para verificar si una lista existe por su ID
        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.ListId == id); // Devuelve true si la lista existe, false de lo contrario.
        }
    }
}
