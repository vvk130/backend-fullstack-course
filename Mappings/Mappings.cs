using AutoMapper;
using GameModel;

namespace GameModel{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Horse, HorseShortDto>().ReverseMap();
        CreateMap<Horse, HorseCreateDto>().ReverseMap();

        CreateMap<Alpaca, AlpacaCreateDto>().ReverseMap();

        CreateMap<Alpaca, AlpacaShortDto>()
            .ConstructUsing(a => new AlpacaShortDto(
                a.Id,
                a.Name,
                a.AlpacaBreed,
                a.Gender,
                a.ImgUrl
            ));

        CreateMap<Competition, CompetitionDto>().ReverseMap();
        CreateMap<Competition, CompetitionCreateDto>().ReverseMap();
        
        CreateMap<PuzzleAnswer, PuzzleAnswerShortDto>().ReverseMap();
        CreateMap<PuzzleAnswer, PuzzleAnswerCreateDto>().ReverseMap();

        CreateMap<SalesAd, SalesAdDto>().ReverseMap();
        CreateMap<SalesAd, SalesAdCreateDto>().ReverseMap();

        CreateMap(typeof(SalesAdShortDto<>), typeof(SalesAdShortDto<>)); 

        CreateMap<Wallet, WalletDto>().ReverseMap();
        CreateMap<Wallet, WalletCreateDto>().ReverseMap();

        CreateMap<Question, QuestionDto>().ReverseMap();
        CreateMap<Question, QuestionCreateDto>().ReverseMap();

        CreateMap<Option, OptionDto>().ReverseMap();

        CreateMap<StockImg, StockImgDto>().ReverseMap();

        CreateMap<AlpacaCreateDto, Animal>().ReverseMap().IncludeAllDerived();
        CreateMap<AlpacaCreateDto, Alpaca>().ReverseMap();

    }
}
}

