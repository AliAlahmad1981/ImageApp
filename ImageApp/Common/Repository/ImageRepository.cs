using ImageApp.Common.Repository.Interfaces;
using ImageApp.Context;
using ImageApp.Entites;
using Microsoft.EntityFrameworkCore;

namespace ImageApp.Common.Repository;

public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;

    public ImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Image>> GetAllImages(CancellationToken token = default) =>
        await _context.Set<Image>().ToListAsync(token);


    public async Task<Image?> GetImageByName(string fileName)
        => await _context.Set<Image>()
            .FirstOrDefaultAsync(x => x.FileName.ToUpper().Contains(fileName.ToUpper()));

    public async Task<Image?> GetImageById(int id)
        => await _context.Set<Image>().FirstOrDefaultAsync(x => x.Id == id);

    public async Task SaveImage(Image image)
    {
        await _context.Set<Image>().AddAsync(image);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteImage(Image image)
    {
        _context.Set<Image>().Remove(image);
        await _context.SaveChangesAsync();
    }
}