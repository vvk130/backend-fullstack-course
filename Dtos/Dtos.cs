namespace GameModel{

public record HorseShortDto(Guid Id, string Name, Breed breed, Gender gender, string ImgUrl);
public record HorseCreateDto(string Name, string ImgUrl);

public record AlpacaShortDto(Guid Id, string Name, AlpacaBreed breed, Gender gender, string ImgUrl);

public record AlpacaCreateDto(
    string Name,
    string? ImgUrl,
    double Age,
    Gender Gender,
    int Capacity,
    int Relationship,
    int Energy,
    Guid? OwnerId = null
);

public record CompetitionDto(Guid Id, CompetitionType CompetitionType, DateTime EndTime);
public record CompetitionCreateDto(CompetitionType CompetitionType, DateTime StartTime, DateTime EndTime);

public record BreedShortDto(Breed Breed);

public record LevelShortDto(int LevelNumber, int EntryPoints);

public record class FileUploadRequestDto
{
    public required IFormFile File { get; init; }
    public required string FolderName { get; init; }
}

public record HorseFilterDto
{
    public List<Gender>? Genders { get; init; }
    public List<Breed>? Breeds { get; init; }
    public double? MinAge { get; init; }
    public double? MaxAge { get; init; }
    public Guid? OwnerId { get; init; }
    public Guid? SireId { get; init; }
    public Guid? DamId { get; init; }
}

// public record CompFilterDto
// {
//     public List<Gender>? Genders { get; init; }
//     public List<Breed>? Breeds { get; init; }
//     public double? MinAge { get; init; }
//     public double? MaxAge { get; init; }
//     public Guid? OwnerId { get; init; }
// }

// public record SalesAdFilterDto
// {

// }

public record ItemCreatedEvent(
    Guid ItemId,
    string ItemName
);

public record PuzzleAnswerShortDto(Guid Id);
public record PuzzleAnswerCreateDto(List<PuzzlePieceDto> Pieces);

public record PuzzleCorrectionRequest(Guid Id, List<PuzzlePieceDto> Pieces);

public record PuzzleUnsolved(Guid Id, List<string> Pieces);

public record PuzzlePieceDto(int XCoordinate, int YCoordinate, string ImgUrl);

public record RankedHorse(int Ranking, HorseShortDto Horse, Guid? OwnerId, Guid CompetitionId, int MoneyWon);

public record CompetitionResult(List<RankedHorse> Results);

public record WalletDto(Guid Id, int Balance, Guid OwnerId);
public record WalletCreateDto(int Balance, Guid OwnerId);

public record SalesAdDto(Guid Id, int Price, DateTime EndTime, Guid HorseId, Guid OwnerId);
public record SalesAdWithHorseDto(Guid Id, AdType type, int Price, DateTime EndTime, Guid OwnerId, HorseShortDto? horse);
public record SalesAdShortDto<TDto>(
    Guid Id,
    AdType AdType,
    int Price,
    DateTime EndTime,
    Guid OwnerId,
    TDto? Item
);

public record SalesAdCreateDto(int Price, DateTime EndTime, Guid HorseId, Guid OwnerId);

public record CompResultDto(Guid Id, Guid HorseId, string HorseName, int Ranking, double MoneyWon);

public record CompResultStatisticsDto(Guid HorseId, double TotalMoneyWon, double AverageRanking, double BestRanking, int CompEntriesCount);

public record OptionDto(string Text, bool IsRightAnswer);

public record QuestionDto(Guid Id, string QuestionSentence, IList<OptionDto> Options, int Difficulty);
public record QuestionCreateDto(string QuestionSentence, IList<OptionDto> Options, int Difficulty);

public record StockImgDto(string imgUrl);
}