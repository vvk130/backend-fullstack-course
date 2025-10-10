// using FluentValidation;
// using GameModel;

// public class HorseValidator : AbstractValidator<Horse>
// {
//     public FileUploadRequestDtoValidator()
//     {
//         RuleFor(x => x.File)
//             .NotNull().WithMessage("File is required.")
//             .Must(file => file.ContentType == "image/jpeg")
//             .WithMessage("Only JPEG allowed.")
//             .Must(file => file.Length <= MaxFileSizeBytes)
//             .WithMessage($"File must not exceed {MaxFileSizeKB}KB.");
//     }
// }