using AutoMapper;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;

namespace DOTNET_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Fighter, GetFighterDto>();
            CreateMap<AddFighterDto, Fighter>();
        }
    }
}