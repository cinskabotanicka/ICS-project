using System.Collections.ObjectModel;

namespace Trackit.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }

    public ObservableCollection<UsersInProjectListModel> Users { get; init; } = new();

    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();


    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty
    };
}
