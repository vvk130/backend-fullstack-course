public class PuzzleService : IPuzzleService
{
    private readonly List<PuzzlePiece> _puzzleAnswers;

    public PuzzleService()
    {
        _puzzleAnswers = GeneratePuzzlePieces(); 
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
            {
                return false; 
            }
        }

        return true; 
    }

    // public PuzzleGenerator(){
    // PuzzleImageResizer() --to 500x500
    // GeneratePuzzlePieces(string ImgUrl)
    // }
    // public PuzzleImageResizer()

    private List<PuzzlePiece> GeneratePuzzlePieces(string ImgUrl)
    {
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
            ).BuildImageTag($"{ImgUrl}");
        }
    }
}
}
