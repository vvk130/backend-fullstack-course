using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

public class PuzzleService : IPuzzleService
{
    private readonly IImageService _imageService;
    private readonly Cloudinary _cloudinary;
    private readonly AppDbContext _context;

    public PuzzleService(IImageService imageService, Cloudinary cloudinary, AppDbContext context)
    {
        _imageService = imageService;
        _cloudinary = cloudinary;
        _context = context;
    }

    public async Task<bool> CheckAllPieces(PuzzleCorrectionRequest request)
    {
        
        var puzzleAnswer = await _context.PuzzleAnswers.FindAsync(request.Id);
        if (puzzleAnswer == null)
            return false; 
        
        foreach (var requestPiece in request.Pieces)
        {
            var piece = request.Pieces.FirstOrDefault(p => p.ImgUrl == requestPiece.ImgUrl);

            if (piece == null || piece.XCoordinate != requestPiece.XCoordinate || piece.YCoordinate != requestPiece.YCoordinate)
                return false; 
        }

        return true; 
    }

    public async Task<OperationResult<PuzzleUnsolved>> PuzzleGenerator(string originalImgUrl)
    {
        var result = new OperationResult<PuzzleUnsolved>();

        var pieces = GeneratePuzzlePieces(originalImgUrl);

        var puzzleAnswer = new PuzzleAnswer
        {
            PuzzlePieces = pieces
        };

        _context.PuzzleAnswers.Add(puzzleAnswer);
        await _context.SaveChangesAsync();

        var puzzleUnsolved = new PuzzleUnsolved(
                puzzleAnswer.Id,
                puzzleAnswer.PuzzlePieces.Select(p => p.ImgUrl).ToList()
            );

        result.Value = puzzleUnsolved;

        return result;
    }

    private List<PuzzleAnswer.PuzzlePiece> GeneratePuzzlePieces(string originalImgUrl)
    {
        List<PuzzleAnswer.PuzzlePiece> puzzlePieces = new();        
        
        for (int i = 0; i < 10; i++) 
        {
            for (int j = 0; j < 10; j++) 
            {
                var x = j * 50; 
                var y = i * 50;

                var imgUrl = _cloudinary.Api.UrlImgUp.Transform(new Transformation()
                    .Width(50)   
                    .Height(50)  
                    .X(x)        
                    .Y(y)       
                    .Crop("crop") 
                ).BuildImageTag(originalImgUrl);

                puzzlePieces.Add(new PuzzleAnswer.PuzzlePiece
                {
                    XCoordinate = x,
                    YCoordinate = y,
                    ImgUrl = imgUrl
                });
            }
        }

        return puzzlePieces;
    }
}
