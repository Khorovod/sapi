using AutoMapper;
using SimpleAPI.Models;
using SimpleAPI.DataTransferObjects;
using System.Collections.Generic;

namespace SimpleAPI.DtoProfiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>().ReverseMap();
        }
    }
}