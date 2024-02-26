using AutoMapper;
using Todo_List_WebApp.Models;
using TodoList_MVC.Models.DTOs;

namespace TodoList_MVC.Models.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<TaskModel, TaskDTO>();
            CreateMap<TaskDTO, TaskModel>();

            CreateMap<UserModel, UserRegisterDTO>();
            CreateMap<UserRegisterDTO, UserModel>();

            CreateMap<UserModel, UserLoginDTO>();
            CreateMap<UserLoginDTO, UserModel>();

        }
    }
}
