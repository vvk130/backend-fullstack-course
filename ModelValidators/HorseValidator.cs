// public class HorseValidator : AbstractValidator<Horse>
// {
//     private readonly IHorseBreedService _horseBreedService;

//     public HorseValidator(IHorseBreedService horseBreedService)
//     {
//         _horseBreedService = horseBreedService;

//         RuleFor(h => h.Breed)
//             .Must(breed => _horseBreedService.GetPossibleColors(breed).Any());

//         RuleFor(h => h)
//             .Must(horse =>
//                 _horseBreedService.GetPossibleColors(horse.Breed).Contains(horse.Color));
//     }
// }
