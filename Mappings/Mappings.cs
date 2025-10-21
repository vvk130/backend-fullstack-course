using AutoMapper;
using GameModel;

namespace GameModel{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Horse, HorseShortDto>().ReverseMap();
        CreateMap<Competition, CompetitionDto>().ReverseMap();
        CreateMap<Breed, BreedShortDto>().ReverseMap();
        CreateMap<Level, LevelShortDto>().ReverseMap();
        CreateMap<PuzzleAnswer, PuzzleAnswerShortDto>().ReverseMap();
        CreateMap<SalesAd, SalesAdDto>().ReverseMap();
        CreateMap<Wallet, WalletDto>().ReverseMap();
    }
}
}

