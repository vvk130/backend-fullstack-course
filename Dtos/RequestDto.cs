namespace GameModel{
    public record PaginationRequest(int PageNumber = 1, int PageSize = 10);

// public record PaginationSearchRequest(HorseFilterDto filter, int PageNumber = 1, int PageSize = 10);

    public record SalesAdRequest(int Price, AdType AdType, DateTime EndTime, Guid HorseId, Guid OwnerId);

    public record BuyRequest(Guid BuyerId, Guid AdId);

    public record CompResultStatisticsRequest(Guid HorseId);

    public record CompetitionRequest(Guid CompetitionId, List<Guid> HorseIds);
}