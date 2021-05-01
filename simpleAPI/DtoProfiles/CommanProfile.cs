using AutoMapper;
using SimpleAPI.Models;
using SimpleAPI.DataTransferObjects;

namespace SimpleAPI.DtoProfiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>();
        }
    }
}