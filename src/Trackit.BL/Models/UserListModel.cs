namespace Trackit.BL.Models;

public record UserListModel : ModelBase
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? PhotoUrl { get; set; }


    public static UserListModel Empty => new()
    {
        Id = Guid.Empty,
        FirstName = string.Empty,
        LastName = string.Empty,
        PhotoUrl = string.Empty
    };
}