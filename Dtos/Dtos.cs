namespace GameModel{
public record HorseShortDto(Guid Id, string Name, string ImgUrl);

public record CompetitionDto(Guid Id, string Name, CompetitionType CompetitionType, DateTime EndTime);

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

}
