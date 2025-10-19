using AutoMapper;
using GameModel;

namespace GameModel{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Horse, HorseShortDto>();
        CreateMap<Competition, CompetitionDto>();
        CreateMap<Breed, BreedShortDto>();
        CreateMap<Level, LevelShortDto>();
        CreateMap<PuzzleAnswer, PuzzleAnswerShortDto>();
        CreateMap<SalesAd, SalesAdDto>();
    }
}
}

