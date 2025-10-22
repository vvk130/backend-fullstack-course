using AutoMapper;
using GameModel;

namespace GameModel{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Horse, HorseShortDto>().ReverseMap();
        CreateMap<Horse, HorseCreateDto>().ReverseMap();

        CreateMap<Competition, CompetitionDto>().ReverseMap();
        CreateMap<Competition, CompetitionCreateDto>().ReverseMap();
        
        // CreateMap<Level, LevelShortDto>().ReverseMap();
        // CreateMap<Level, LevelCreateDto>().ReverseMap();
        
        // CreateMap<Breed, BreedShortDto>().ReverseMap();
        // CreateMap<Breed, BreedCreateDto>().ReverseMap();
        
        CreateMap<PuzzleAnswer, PuzzleAnswerShortDto>().ReverseMap();
        CreateMap<PuzzleAnswer, PuzzleAnswerCreateDto>().ReverseMap();

        CreateMap<SalesAd, SalesAdDto>().ReverseMap();
        CreateMap<SalesAd, SalesAdCreateDto>().ReverseMap();

        CreateMap<Wallet, WalletDto>().ReverseMap();
        CreateMap<Wallet, WalletCreateDto>().ReverseMap();

        CreateMap<Question, QuestionDto>().ReverseMap();
        CreateMap<Question, QuestionCreateDto>().ReverseMap();

        CreateMap<Option, OptionDto>().ReverseMap();
    }
}
}

