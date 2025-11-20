namespace GameModel{
    public record PaginationRequest(int PageNumber = 1, int PageSize = 10);

    public record PaginationSearchRequest(HorseFilterDto Filter, PaginationRequest Pagination);

    public record PaginationAlpacaSearchRequest(AlpacaFilterDto Filter, PaginationRequest Pagination);

    public record SalesAdRequest(int Price, AdType AdType, int daysAdIsValid, Guid HorseId, Guid OwnerId, ItemType ItemType);

    public record BuyRequest(Guid BuyerId, Guid AdId, ItemType ItemType, int Bid = 0);

    public record CompResultStatisticsRequest(Guid HorseId);

    public record CompetitionFrontEndRequest(Guid CompetitionId, Guid HorseId1, Guid HorseId2, Guid HorseId3);

    public record CompetitionRequest(Guid CompetitionId, List<Guid> HorseIds);

    public record FoalHorseRequestDto(Guid SireId, Guid DamId, ItemType type);
}