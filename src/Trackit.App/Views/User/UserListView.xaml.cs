using Trackit.App.ViewModels.User;

namespace Trackit.App.Views.User;

public partial class UserListView
{
	public UserListView(UserListViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
	}
}