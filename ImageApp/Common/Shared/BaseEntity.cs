namespace ImageApp.Common.Shared;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } =DateTime.Now;
}