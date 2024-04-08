namespace WebShop.Services.ImageService
{
    public interface IImageService
    {
        Task<string> UploadPhoto(IFormFile file);
    }
}
