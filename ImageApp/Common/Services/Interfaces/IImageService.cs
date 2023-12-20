using ImageApp.Entites;

namespace ImageApp.Common.Services.Interfaces;

public interface IImageService
{
     Task<List<Image>> GetAllImages(CancellationToken token = default);
    Task<Image> GetImageByName(string fileName);
    Task<Image> GetImageById(int id);
    Task<bool> SaveImage(Image images);
    Task<bool> DeleteImage(Image images);
}