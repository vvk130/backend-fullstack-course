using Microsoft.AspNetCore.Mvc;
using GameModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace YourProject.Controllers
{
    [Route("api/competitions")]
    public class CompetitionsController : GenericController<Competition, CompetitionCreateDto, CompetitionDto>
    {
        private readonly ICompetitionService _competitionService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CompetitionsController(ICompetitionService competitionService, IGenericService<Competition> service, IMapper mapper, AppDbContext context) : base(service, mapper) 
        { 
            _competitionService = competitionService;
            _context = context;
        }

            [HttpPost("compete-horses")]
            public async Task<IActionResult> CompeteHorses([FromBody] CompetitionFrontEndRequest request, CancellationToken ct)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var horseIds = new List<Guid> { request.HorseId1, request.HorseId2, request.HorseId3 };

                var result = await _competitionService.GetCompetitionResult(request.CompetitionId, horseIds, ct);
                return Ok(result);
            }

            [HttpGet("dto/{id}")]
            public async Task<IActionResult> GetCompetition(Guid id)
            {
                var competition = await _context.Competitions.FindAsync(id);
                if (competition == null)
                    return NotFound();

                var now = DateTime.UtcNow;

                var dto = new CompetitionCreateDto(
                    competition.CompetitionType,
                    DaysToStart: (int)Math.Round((competition.StartTime - now).TotalDays),
                    DaysToEnd: (int)Math.Round((competition.EndTime - now).TotalDays)
                );

                return Ok(dto);
            }


            [HttpPut("{id}")]
            public override async Task<IActionResult> Update(Guid id, [FromBody] CompetitionCreateDto dto)
            {
                var competition = await _context.Competitions.FindAsync(id);
                if (competition == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return ValidationProblem(ModelState);

                competition.CompetitionType = dto.CompetitionType;
                competition.StartTime = DateTime.UtcNow.AddDays(dto.DaysToStart);
                competition.EndTime = DateTime.UtcNow.AddDays(dto.DaysToEnd);

                await _context.SaveChangesAsync();

                return Ok(competition);
            }

            [HttpPost]
            public override async Task<IActionResult> Create([FromBody] CompetitionCreateDto dto)
            {
                if (!ModelState.IsValid)
                    return ValidationProblem(ModelState);

                var competition = new Competition
                {
                    CompetitionType = dto.CompetitionType,
                    StartTime = DateTime.UtcNow.AddDays(dto.DaysToStart),
                    EndTime = DateTime.UtcNow.AddDays(dto.DaysToEnd)
                };

                _context.Competitions.Add(competition);
                await _context.SaveChangesAsync();

                return Ok(competition); 
            }

    }

    [Route("api/wallet")]
    public class WalletController : GenericController<Wallet, WalletCreateDto, WalletDto>
    {
        private readonly IMapper _mapper;

        public WalletController(IGenericService<Wallet> service, IMapper mapper) : base(service, mapper) {}

        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<IActionResult> Delete(Guid id)
        {
            return BadRequest("Delete operation is not allowed for this entity.");
        }
    }

    [Route("api/alpacas")]
    public class AlpacasController : GenericController<Alpaca, AlpacaCreateDto, AlpacaShortDto>
    {
        private readonly IMapper _mapper;

        public AlpacasController(IGenericService<Alpaca> service, IMapper mapper) : base(service, mapper) {}

        // [HttpGet("search")]
        // public async Task<IActionResult> SearchAlpacas([FromQuery] PaginationSearchRequest request)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState); 

        //     var filter = request.Filter;

        //     Expression<Func<Alpaca, bool>> predicate = h =>
        //         (filter.Genders == null || filter.Genders.Contains(h.Gender)) &&
        //         (filter.AlpacaBreeds == null || filter.AlpacaBreeds.Contains(h.AlpacaBreed)) &&
        //         (!filter.MinAge.HasValue || h.Age >= filter.MinAge) &&
        //         (!filter.MaxAge.HasValue || h.Age <= filter.MaxAge) &&
        //         (!filter.OwnerId.HasValue || h.OwnerId == filter.OwnerId) &&
        //         (!filter.SireId.HasValue || h.SireId == filter.SireId) &&
        //         (!filter.DamId.HasValue || h.DamId == filter.DamId);

        //     var result = await _genericService.GetPaginatedAsync<AlpacaShortDto>(request.Pagination, predicate);
        //     return Ok(result);
        // }

    }

    [Route("api/salesads")]
    public class SalesAdsController : GenericController<SalesAd, SalesAdCreateDto, SalesAdDto>
    {
            private readonly IGenericService<SalesAd> _adService;
            private readonly IGenericService<Horse> _horseService;
            private readonly IGenericService<Wallet> _walletService;
            private readonly AppDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public SalesAdsController(
                AppDbContext context,
                UserManager<ApplicationUser> userManager,
                IGenericService<SalesAd> adService,
                IGenericService<Wallet> walletService,
                IGenericService<Horse> horseService,
                IMapper mapper) : base(adService, mapper)
            {
                _adService = adService;
                _horseService = horseService;
                _walletService = walletService;
                _context = context;
                _userManager = userManager;
            }

            [HttpPost("create-sales-ad")]
            public async Task<ActionResult<OperationResult<SalesAd>>> Create([FromBody] SalesAdRequest request)
            {
                var result = new OperationResult<SalesAd>();
                
                // if (!ModelState.IsValid)
                //     return BadRequest(ModelState);

                var ifExists = false;

                if (request.ItemType == ItemType.Horse){
                    ifExists = await _context.Horses
                        .AnyAsync(h => h.Id == request.HorseId && h.OwnerId == request.OwnerId);
                }
                if (request.ItemType == ItemType.Alpaca){
                    ifExists = await _context.Alpacas
                        .AnyAsync(h => h.Id == request.HorseId && h.OwnerId == request.OwnerId);
                }

                if (!ifExists){
                    result.AddError(nameof(request.HorseId), "You don't own this animal.");
                    return BadRequest(result);
                }

                var ads = await _adService.FindAsync(a =>
                    a.HorseId == request.HorseId && a.EndTime > DateTime.UtcNow);

                if (ads.Any()){
                    result.AddError(nameof(request.HorseId), "This animal is already for sale, go to modify ad instead.");
                    return BadRequest(result);
                }

                var newAd = new SalesAd
                {
                    HorseId = request.HorseId,
                    OwnerId = request.OwnerId,
                    Price = request.Price,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(request.daysAdIsValid),
                    AdType = request.AdType,
                    ItemType = request.ItemType
                };

                await _adService.AddAsync(newAd);
                result.Value = newAd; 
                return Ok(result);
            }

            [HttpPost("resolve-auctions")]
            public async Task<IActionResult> ResolveAuctions()
            {
                var ads = await _adService.FindAsync(a => 
                    a.AdType == AdType.Auction && a.EndTime < DateTime.UtcNow);

                if (ads == null || !ads.Any())
                    return Ok("No expired auction ads to resolve.");

                foreach (var ad in ads)
                {
                    var horse = await _context.Animals.FindAsync(ad.HorseId);
                    if (horse == null)
                    {
                        continue;
                    }

                    if (ad.HighestBidderId != null && horse != null)
                    {
                        horse.OwnerId = ad.HighestBidderId.Value;
                    }

                    _context.SalesAds.Remove(ad);
                    await _context.SaveChangesAsync();
                }

                return Ok("All expired auction ads resolved successfully.");
            }

            [HttpPost("buy-horse")]
            public async Task<IActionResult> BuyHorse([FromBody] BuyRequest request)
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var ad = await _context.SalesAds.FindAsync(request.AdId);
                    if (ad is null)
                        return BadRequest("Add not found.");

                    var user = await _userManager.FindByIdAsync(request.BuyerId.ToString());
                    if (user is null)
                        return BadRequest("User not found.");

                    var wallet = await _context.Wallets.Where(w => w.OwnerId == request.BuyerId).FirstOrDefaultAsync();
                    if (wallet is null)
                    {
                        wallet = new Wallet {
                            OwnerId = request.BuyerId,
                            Balance = 5000,
                        };
                        await _walletService.AddAsync(wallet);
                    }

                    Animal? horse = null;
                    if (request.ItemType == ItemType.Horse)
                    {
                        horse = await _context.Horses.FindAsync(ad.HorseId);
                    }
                    if (request.ItemType == ItemType.Alpaca)
                    {
                        horse = await _context.Alpacas.FindAsync(ad.HorseId);
                    }
                    if (horse is null)
                        return BadRequest("Animal not found.");

                    if (ad.AdType is not AdType.Auction)
                        wallet.Balance -= ad.Price;

                    if (ad.AdType is AdType.Auction)
                        wallet.Balance -= request.Bid;

                    if(wallet.Balance < 0)
                        return BadRequest("Insufficient funds");

                    if (ad.AdType is not AdType.Auction)
                        horse.OwnerId = request.BuyerId;

                    if (ad.AdType is AdType.Auction && request.Bid < (ad.Price+200))
                        return BadRequest("Bid needs to be 200 higher than previous price/bid");

                    if (ad.AdType is AdType.Auction){
                        var prevBidderWallet = await _context.Wallets.Where(w => w.OwnerId == ad.HighestBidderId).FirstOrDefaultAsync();
                        if (prevBidderWallet is not null)
                        {
                            prevBidderWallet.Balance += ad.Price;
                        }
                    }
                    if (ad.AdType is AdType.Auction)
                    {
                        ad.HighestBidderId = request.BuyerId;
                        ad.Price = request.Bid;
                    }
                    if (ad.AdType is not AdType.Auction){
                        var sellerWallet = await _context.Wallets.Where(w => w.OwnerId == ad.OwnerId).FirstOrDefaultAsync();
                        if (sellerWallet is not null)
                        {
                            sellerWallet.Balance += ad.Price;
                        }
                    }
                    if (ad.AdType is not AdType.Auction)
                        _context.SalesAds.Remove(ad);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var message = $"You bought animal with id {ad.HorseId}";
                    if (ad.AdType is AdType.Auction)
                        message = $"Your offer was placed successfully on {ad.HorseId}";

                    return Ok($"{message}");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Transaction failed {ex.Message}");
                }
            }
            [HttpGet("paginated-with-horse")]
            public async Task<IActionResult> PaginatedWithHorse([FromQuery] PaginationRequest request)
            {

                var query = from ad in _context.SalesAds
                            join horse in _context.Horses
                                on ad.HorseId equals horse.Id
                            select new { Ad = ad, Horse = horse };

                var totalCount = await query.CountAsync();

                var list = await query
                    .OrderBy(x => x.Ad.Price)      
                    .ThenBy(x => x.Ad.EndTime)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var items = list.Select(x => new SalesAdWithHorseDto(
                    x.Ad.Id,
                    x.Ad.AdType,
                    x.Ad.Price,
                    x.Ad.EndTime,
                    x.Ad.OwnerId,
                    new HorseShortDto(
                        x.Horse.Id,
                        x.Horse.Name,
                        x.Horse.Breed,
                        x.Horse.Gender,
                        x.Horse.ImgUrl
                    )
                )).ToList();

                return Ok(new PaginatedResult<SalesAdWithHorseDto>(items, totalCount, request.PageNumber, request.PageSize));
            }
    }

    [Route("api/questions")]
    public class QuestionsController : GenericController<Question, QuestionCreateDto, QuestionDto>
    {
            private readonly IGenericService<Question> _adService;
            // private readonly AppDbContext _context;
            // private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public QuestionsController(
                AppDbContext context,
                // UserManager<ApplicationUser> userManager,
                IGenericService<Question> adService,
                IMapper mapper) : base(adService, mapper)
            {
                // _adService = adService;
                // _horseService = horseService;
                // _walletService = walletService;
                // _context = context;
                // _userManager = userManager;
            }

    }

    [Route("api/stockImgs")]
    public class StockImgsController : GenericController<StockImg, StockImgDto, StockImgDto>
    {
            private readonly IGenericService<StockImg> _stockImgService;
            private readonly IMapper _mapper;
            private readonly AppDbContext _context;

            public StockImgsController(
                AppDbContext context,
                IGenericService<StockImg> stockImgService,
                IMapper mapper) : base(stockImgService, mapper)
            {
                _context = context;
            }

            [HttpPut("add-Img-If-Null")]
            public async Task<IActionResult> AddImgToAllIfNull()
            {
                var horsesWithoutImage = await _context.Horses
                    .ToListAsync();

                var stockImages = await _context.StockImgs
                    .Select(si => si.ImgUrl)
                    .ToListAsync();

                var rnd = new Random();

                foreach (var horse in horsesWithoutImage)
                {
                    horse.ImgUrl = stockImages[rnd.Next(stockImages.Count)];
                }

                await _context.SaveChangesAsync();
                return Ok("Done");
            }

    }
}
