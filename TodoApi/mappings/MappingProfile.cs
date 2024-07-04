using AutoMapper;
using TodoApi.Models;
using ToDoList.DTO;

namespace TodoApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<List, ListDto>().ReverseMap();
            CreateMap<TodoApi.Models.Task, TaskDto>().ReverseMap();
        }
    }
}

