using ImageApp.Entites;

namespace ImageApp.Common.Repository.Interfaces;

public interface IImageRepository
{
    Task<List<Image>> GetAllImages(CancellationToken token = default);
    Task<Image?> GetImageByName(string fileName);
    Task<Image?> GetImageById(int id);
    Task SaveImage(Image images);
    Task DeleteImage(Image image);
}