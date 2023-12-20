using ImageApp.Common.Services.Interfaces;
using ImageApp.Entites;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers;

[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _environment;

    public ImagesController(IImageService imageService, IWebHostEnvironment environment)
    {
        _imageService = imageService;
        _environment = environment;
    }

    // GET
    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _imageService.GetAllImages());
    }

    [HttpGet("ByName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var image = await _imageService.GetImageByName(name);
        return image is not null ? Ok(image) : BadRequest("Image Not Found");
    }

    [HttpGet("ById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var image = await _imageService.GetImageById(id);
        return image is not null ? Ok(image) : BadRequest("Image Not Found");
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file, string description)
    {
        if (file is null || string.IsNullOrEmpty(description))
            return BadRequest("Invalid Input");

        var filePath = Path.Combine(_environment.WebRootPath, "Images", file.FileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
            await file.CopyToAsync(stream);
        var result = await _imageService.SaveImage(new Image()
        {
            Description = description,
            ContentType = file.ContentType,
            FileExtension = Path.GetExtension(filePath),
            FileName = file.FileName
        });
        return result ? Ok("Images Saved Successfully") : BadRequest("Image Saved Failed");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var image = await _imageService.GetImageById(id);
        var filePath = Path.Combine(_environment.WebRootPath, "Images", image.FileName);
        var deleted = await _imageService.DeleteImage(image);
        if (!deleted) return BadRequest("Image Deleted Failed");

        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        return Ok("Image Deleted Successfully");
    }
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        var image = await _imageService.GetImageById(id);
        if (image is null) return NotFound("Image Not Found ");

        var filePath = Path.Combine(_environment.WebRootPath, "Images", image.FileName);
        var stream = new FileStream(filePath, FileMode.Open);
        return File(stream, image.ContentType);
    }
}