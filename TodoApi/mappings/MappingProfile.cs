using AutoMapper;
using TodoApi.Models;
using ToDoList.DTO;

namespace TodoApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configurar mapeos
            CreateMap<TodoTask, TodoItem>().ReverseMap();
            CreateMap<List, ListDto>().ReverseMap();
            CreateMap<TodoApi.Models.Task, TaskDto>().ReverseMap();
        }
    }
}
