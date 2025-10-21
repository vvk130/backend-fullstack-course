using Microsoft.AspNetCore.Mvc;
using GameModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AutoMapper;

namespace YourProject.Controllers
{
    [Route("api/competitions")]
    [Authorize]
    public class CompetitionsController : GenericController<Competition, CompetitionDto>
    {
        private readonly ICompetitionService _competitionService;
        private readonly IMapper _mapper;

        public CompetitionsController(ICompetitionService competitionService, IGenericService<Competition> service, IMapper mapper) : base(service, mapper) 
        { 
            _competitionService = competitionService;
        }

            [HttpPost("compete-horses")]
            public async Task<IActionResult> CompeteHorses([FromBody] CompetitionRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _competitionService.GetCompetitionResult(request.CompetitionId, request.HorseIds);
                return Ok(result);
            }

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/horsebreeds")]
    public class HorseBreedsController : GenericController<HorseBreed, BreedShortDto>
    {
        private readonly IMapper _mapper;

        public HorseBreedsController(IGenericService<HorseBreed> service, IMapper mapper) : base(service, mapper) {}

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/levels")]
    public class LevelsController : GenericController<Level, LevelShortDto>
    {
        private readonly IMapper _mapper;
        
        public LevelsController(IGenericService<Level> service, IMapper mapper) : base(service, mapper) { }

        // [HttpPut("clean-stable")]
        // public override async Task<IActionResult> CleanStable(Guid userId)
        // {
            
        // }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id)
        {
            return BadRequest("Delete operation is not allowed for this entity.");
        }
    }

    [Route("api/wallet")]
    public class WalletController : GenericController<Wallet, WalletDto>
    {
        private readonly IMapper _mapper;

        public WalletController(IGenericService<Wallet> service, IMapper mapper) : base(service, mapper) {}

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }

    [Route("api/salesad")]
    public class SalesAdController : GenericController<SalesAd, SalesAdDto>
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

            [Authorize]
            [HttpPost("create-sales-ad")]
            public async Task<IActionResult> Create([FromBody] SalesAdRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.GetUserAsync(User);

                if (user is null)
                    Forbid("User not found.");

                var userId = user.Id;

                var horse = await _horseService.FindAsync(h =>
                    h.Id == request.HorseId && h.OwnerId == Guid.Parse(userId));

                if (horse is null)
                    return Forbid("You don't own this horse.");

                var ad = new SalesAd
                {
                    HorseId = request.HorseId,
                    OwnerId = request.OwnerId,
                    Price = request.Price,
                    StartTime = DateTime.UtcNow,
                    EndTime = request.EndTime,
                    AdType = request.AdType
                };

                await _adService.AddAsync(ad);
                return Ok(ad);
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

            [HttpDelete("{id}")]
            public override async Task<IActionResult> Delete(Guid id)
            {
                return BadRequest("Delete operation is not allowed for this entity.");
            }

    }
}
