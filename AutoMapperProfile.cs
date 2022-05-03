using AutoMapper;
using DOTNET_RPG.Dtos.Chant;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Dtos.Skill;
using DOTNET_RPG.Models;

namespace DOTNET_RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Fighter, GetFighterDto>();
            CreateMap<AddFighterDto, Fighter>();
            CreateMap<Chant, GetChantDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}