using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using ToDoList.DTO;
using AutoMapper;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")] // Define la ruta base para las solicitudes a este controlador.
    [ApiController] // Indica que este controlador responderá a solicitudes HTTP y facilitará la validación automática del modelo.
    public class TasksController : ControllerBase
    {
        private readonly TodoContext _context; // Contexto de la base de datos para interactuar con las entidades.
        private readonly IMapper _mapper; // Mapper para convertir entre modelos y DTOs.

        public TasksController(TodoContext context, IMapper mapper)
        {
            _context = context; // Inicializa el contexto de la base de datos.
            _mapper = mapper; // Inicializa el mapper.
        }

        // Método GET para obtener todas las tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync(); // Obtiene todas las tareas de manera asincrónica.
            return Ok(_mapper.Map<IEnumerable<TaskDto>>(tasks)); // Mapea las tareas a DTOs y las devuelve en la respuesta.
        }

        // Método GET para obtener una tarea por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id); // Busca la tarea por su ID de manera asincrónica.

            if (task == null)
            {
                return NotFound(); // Devuelve un 404 si no se encuentra la tarea.
            }

            return Ok(_mapper.Map<TaskDto>(task)); // Mapea la tarea a un DTO y la devuelve en la respuesta.
        }

        // Método PUT para actualizar una tarea existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskDto taskDto)
        {
            if (id != taskDto.TaskId)
            {
                return BadRequest(); // Devuelve un 400 si los IDs no coinciden.
            }

            var task = _mapper.Map<TodoApi.Models.Task>(taskDto); // Mapea el DTO a un modelo.
            _context.Entry(task).State = EntityState.Modified; // Marca la tarea como modificada.

            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios de manera asincrónica.
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound(); // Devuelve un 404 si la tarea no existe.
                }
                else
                {
                    throw; // Relanza la excepción si ocurre algún otro error.
                }
            }

            return NoContent(); // Devuelve un 204 si la actualización fue exitosa.
        }

        // Método POST para crear una nueva tarea
        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTask(TaskDto taskDto)
        {
            var task = _mapper.Map<TodoApi.Models.Task>(taskDto); // Mapea el DTO a un modelo.
            _context.Tasks.Add(task); // Añade la nueva tarea al contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios de manera asincrónica.

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, _mapper.Map<TaskDto>(task)); // Devuelve un 201 con la nueva tarea.
        }

        // Método DELETE para eliminar una tarea por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id); // Busca la tarea por su ID de manera asincrónica.
            if (task == null)
            {
                return NotFound(); // Devuelve un 404 si no se encuentra la tarea.
            }

            _context.Tasks.Remove(task); // Elimina la tarea del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios de manera asincrónica.

            return NoContent(); // Devuelve un 204 si la eliminación fue exitosa.
        }

        // Método privado para verificar si una tarea existe por su ID
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id); // Devuelve true si la tarea existe, false de lo contrario.
        }
    }
}
