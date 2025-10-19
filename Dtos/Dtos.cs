namespace GameModel{
public record HorseShortDto(Guid Id, string Name, string ImgUrl);

public record CompetitionDto(Guid Id, CompetitionType CompetitionType, DateTime EndTime);

public record BreedShortDto(Breed Breed);

public record LevelShortDto(int LevelNumber, int EntryPoints);

public record FoalHorseRequestDto(Guid SireId, Guid DamId);

public record class FileUploadRequestDto
{
    public IFormFile File { get; init; }
    public string FolderName { get; init; }
}

public record HorseFilterDto(
    List<Gender>? Genders = null,
    List<Breed>? Breeds = null,
    double? MinAge = null,
    double? MaxAge = null
);

public record ItemCreatedEvent(
    Guid ItemId,
    string ItemName
);

public record PaginationRequest(int PageNumber = 1, int PageSize = 10);

public record PuzzleAnswerShortDto(Guid Id);

public record PuzzleCorrectionRequest(Guid Id, List<PuzzlePieceDto> Pieces);

public record PuzzleUnsolved(Guid Id, List<string> Pieces);

public record PuzzlePieceDto(int XCoordinate, int YCoordinate, string ImgUrl);

public record CompetitionRequest(Guid CompetitionId, List<Guid> HorseIds);

public record RankedHorse(int Ranking, HorseShortDto Horse, Guid? OwnerId, Guid CompetitionId, int MoneyWon);

public record CompetitionResult(List<RankedHorse> Results);

public record WalletDto(Guid Id, int Balance);

public record SalesAdDto(int Price, DateTime EndDate, Guid HorseId, Guid OwnerId);

public record SalesAdRequest(int Price, AdType AdType, DateTime EndTime, Guid HorseId, Guid OwnerId);

public record CompResultDto(Guid Id, Guid HorseId, string HorseName, int Ranking, double MoneyWon);

public record CompResultStatisticsRequest(Guid HorseId);

public record CompResultStatisticsDto(Guid HorseId, double TotalMoneyWon, double AverageRanking, double BestRanking, int CompEntriesCount);
}
