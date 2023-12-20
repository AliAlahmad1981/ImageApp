using ImageApp.Common.Shared;

namespace ImageApp.Entites;

public class Image : BaseEntity
{
    public required string Description { get; set; }
    public required string ContentType { get; set; }
    public required string FileName { get; set; }
    public required string FileExtension { get; set; }
}