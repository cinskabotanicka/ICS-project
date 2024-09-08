using System.Collections.ObjectModel;

namespace Trackit.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? PhotoUrl { get; set; }

    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();


    public static UserDetailModel Empty => new()
    {
        Id = Guid.Empty,
        FirstName = string.Empty,
        LastName = string.Empty,
        PhotoUrl = string.Empty
    };
}
