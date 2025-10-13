namespace GameModel{
public record HorseShortDto(Guid Id, string Name, string ImgUrl);

public record CompetitionDto(Guid Id, CompetitionType CompetitionType, DateTime EndTime);

public record BreedShortDto(Breed Breed);

public record LevelShortDto(int LevelNumber, int EntryPoints);

public record FoalHorseRequestDto(Guid SireId, Guid DamId);

public record class FileUploadRequestDto
{
    public IFormFile File { get; init; }
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

public record PuzzleCorrectionRequest(Guid Id, List<PuzzlePiece> Pieces);

public record PuzzleUnsolved(Guid Id, List<ImgUrl> Pieces);

public record PuzzlePiece(int XCoordinate, int YCoordinate, string ImgUrl);
}
