public class PuzzleService : IPuzzleService
{
    private readonly List<PuzzlePiece> _puzzleAnswers;
    private readonly IImageService _imageService;

    public PuzzleService(IImageService imageService)
    {
        _imageService = imageService;
    }

    public bool CheckAllPieces(PuzzleCorrectionRequest request)
    {
        
        var puzzleAnswer = _puzzleAnswers.FirstOrDefault(p => p.Id == request.Id);
        if (puzzleAnswer == null)
            return false; 
        
        foreach (var requestPiece in request.Pieces)
        {
            var piece = puzzleAnswer.FirstOrDefault(p => p.ImgUrl == requestPiece.ImgUrl);

            if (piece == null || piece.XCoordinate != requestPiece.XCoordinate || piece.YCoordinate != requestPiece.YCoordinate)
                return false; 
        }

        return true; 
    }

    public async Task<OperationResult<PuzzleUnsolved>> PuzzleGenerator(string originalImgUrl)
    {
        var result = new OperationResult<string>();

        var resizeResult = imageService.ImageResizer(originalImgUrl, 500, 500);

        if(!resizeResult.Success)
        {
            result.AddError("file", "Image not found");
            return result;
        }

        var resizedImgUrl = resizeResult.Value;

        var pieces = GeneratePuzzlePieces(string resizedImgUrl);

        var puzzle = new PuzzleUnsolved(Guid.NewGuid(), pieces);

        return puzzle;
    }

    private List<string> GeneratePuzzlePieces(string originalImgUrl)
    {
        List<string> PuzzlePieces = new();        
        
        for (int i = 0; i < 10; i++) 
        {
            for (int j = 0; j < 10; j++) 
            {
                var x = j * 50; 
                var y = i * 50;  

                var imgUrl = cloudinary.Api.UrlImgUp.Transform(new Transformation()
                    .Width(50)   
                    .Height(50)  
                    .X(x)        
                    .Y(y)       
                    .Crop("crop") 
                ).BuildImageTag($"{originalImgUrl}");

                PuzzlePieces.Add(imgUrl);
            }
        }
    }
}
