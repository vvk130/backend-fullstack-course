public record HorseShortDto(Guid Id, string Name, string ImgUrl);

public record CompetitionDto(Guid Id, string Name, CompetitionType CompetitionType, DateTime EndTime);

public record FoalHorseRequestDto(Guid SireId, Guid DamId);

