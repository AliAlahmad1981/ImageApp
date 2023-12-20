using ImageApp.Common.Repository.Interfaces;
using ImageApp.Common.Services.Interfaces;
using ImageApp.Entites;

namespace ImageApp.Common.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;

    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<List<Image>> GetAllImages(CancellationToken token = default)
        => await _imageRepository.GetAllImages(token);

    public async Task<Image> GetImageByName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return null;
        var image = await _imageRepository.GetImageByName(fileName);
        return image;
    }

    public async Task<Image> GetImageById(int id)
    {
        var image = await _imageRepository.GetImageById(id);
        return image;
    }

    public async Task<bool> SaveImage(Image images)
    {
        await _imageRepository.SaveImage(images);
        return true;
    }

    public async Task<bool> DeleteImage(Image image)
    {
        await _imageRepository.DeleteImage(image);
        return true;
    }
}