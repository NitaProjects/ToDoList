using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using ToDoList.DTO;
using AutoMapper;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public ListsController(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListDto>>> GetLists()
        {
            var lists = await _context.Lists
                .Include(l => l.SubLists)
                .Include(l => l.Tasks)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ListDto>>(lists));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListDto>> GetList(int id)
        {
            var list = await _context.Lists
                .Include(l => l.SubLists)
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.ListId == id); 

            if (list == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ListDto>(list));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, ListDto listDto)
        {
            if (id != listDto.ListId)
            {
                return BadRequest();
            }

            var list = _mapper.Map<List>(listDto);
            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ListDto>> PostList(ListDto listDto)
        {
            var list = _mapper.Map<List>(listDto);
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { id = list.ListId }, _mapper.Map<ListDto>(list));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.ListId == id);
        }
    }
}
