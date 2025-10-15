namespace GameModel{
public interface IImageService
{
   Task<OperationResult<string>> UploadImageAsync(IFormFile File, string FolderName);
   Task<OperationResult<string>> ResizeImageAsync(string url, int width, int height);
}
}