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
    [Authorize]
    public class CompetitionsController : GenericController<Competition, CompetitionCreateDto, CompetitionDto>
    {
        private readonly ICompetitionService _competitionService;
        private readonly IMapper _mapper;

        public CompetitionsController(ICompetitionService competitionService, IGenericService<Competition> service, IMapper mapper) : base(service, mapper) 
        { 
            _competitionService = competitionService;
        }

            [HttpPost("compete-horses")]
            public async Task<IActionResult> CompeteHorses([FromBody] CompetitionRequest request, CancellationToken ct)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _competitionService.GetCompetitionResult(request.CompetitionId, request.HorseIds, ct);
                return Ok(result);
            }

    }

    // [Route("api/horsebreeds")]
    // public class HorseBreedsController : GenericController<HorseBreed, HorseCreateBreed, BreedShortDto>
    // {
    //     private readonly IMapper _mapper;

    //     public HorseBreedsController(IGenericService<HorseBreed> service, IMapper mapper) : base(service, mapper) {}

    // }

    // [Route("api/levels")]
    // public class LevelsController : GenericController<Level, LevelCreateDto, LevelShortDto>
    // {
    //     private readonly IMapper _mapper;
        
    //     public LevelsController(IGenericService<Level> service, IMapper mapper) : base(service, mapper) { }

    //     // [HttpPut("clean-stable")]
    //     // public override async Task<IActionResult> CleanStable(Guid userId)
    //     // {
            
    //     // }
    // }

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

    [Route("api/salesad")]
    public class SalesAdController : GenericController<SalesAd, SalesAdCreateDto, SalesAdDto>
    {
            private readonly IGenericService<SalesAd> _adService;
            private readonly IGenericService<Horse> _horseService;
            private readonly IGenericService<Wallet> _walletService;
            private readonly AppDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public SalesAdController(
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
            public async Task<IActionResult> Create([FromBody] SalesAdRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // var user = await _userManager.GetUserAsync(User);

                // if (user is null)
                //     Forbid("User not found.");

                // var userId = user.Id;

                var horse = await _horseService.FindAsync(h =>
                    h.Id == request.HorseId && h.OwnerId == request.OwnerId);

                if (horse is null)
                    return Forbid("You don't own this horse.");

                var ad = await _adService.FindAsync(a =>
                    a.Id == request.HorseId && a.EndTime > DateTime.UtcNow);

                if (ad is not null)
                    return BadRequest("This horse is already for sale, go to modify ad instead.");

                var newAd = new SalesAd
                {
                    HorseId = request.HorseId,
                    OwnerId = request.OwnerId,
                    Price = request.Price,
                    StartTime = DateTime.UtcNow,
                    EndTime = request.EndTime,
                    AdType = request.AdType
                };

                await _adService.AddAsync(newAd);
                return Ok(newAd);
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
                    if (ad.Price > wallet.Balance)
                        return BadRequest("Insufficient funds");

                    var horse = await _context.Horses.FindAsync(ad.HorseId);
                    if (horse is null)
                        return BadRequest("Horse not found.");
        
                    horse.OwnerId = request.BuyerId;
                    wallet.Balance -= ad.Price;
                    if(wallet.Balance < 0)
                    {
                        return BadRequest("Insufficient funds");
                    }
                    _context.SalesAds.Remove(ad);
                    await transaction.CommitAsync();
                    return Ok($"You bought horse with id {ad.HorseId}");
                }
                catch (Exception)
                {
                    return BadRequest("Transaction failed");
                }
            }
    }

    [Route("api/question")]
    public class QuestionController : GenericController<Question, QuestionCreateDto, QuestionDto>
    {
            private readonly IGenericService<Question> _adService;
            // private readonly AppDbContext _context;
            // private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public QuestionController(
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

    [Route("api/stockImg")]
    public class StockImgController : GenericController<StockImg, StockImgDto, StockImgDto>
    {
            private readonly IGenericService<StockImg> _stockImgService;
            private readonly IMapper _mapper;
            private readonly AppDbContext _context;

            public StockImgController(
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
