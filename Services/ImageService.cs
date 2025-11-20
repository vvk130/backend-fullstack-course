using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

public class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;

    public ImageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<OperationResult<string>> ResizeImageAsync(string url, int width, int height)
    {
        var result = new OperationResult<string>();

        result.Value = _cloudinary.Api.UrlImgUp
                            .Transform(new Transformation().Width(width).Height(height).Crop("fill"))
                            .BuildUrl(url);

        return result;

    }

    public async Task<OperationResult<string>> UploadImageAsync(IFormFile file, string folderName)
    {
        var result = new OperationResult<string>();

        if (file == null || file.Length == 0)
        {
            result.AddError("file", "No file uploaded.");
            return result;
        }

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = true,
            Folder = $"{folderName}"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode != HttpStatusCode.OK || uploadResult.SecureUrl == null)
        {
            result.AddError("upload", "Image upload failed.");
            return result;
        }

        result.Value = uploadResult.SecureUrl.AbsoluteUri;
        return result;
    }
}