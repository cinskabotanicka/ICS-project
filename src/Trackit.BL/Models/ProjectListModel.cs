using System.Collections.ObjectModel;

namespace Trackit.BL.Models;

public record ProjectListModel : ModelBase
{
    public required string Name { get; set; }

    public ObservableCollection<UsersInProjectListModel> Users { get; set; } = new();


    public static ProjectListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty
    };
}
