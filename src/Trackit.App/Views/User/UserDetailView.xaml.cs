using Trackit.App.ViewModels.User;

namespace Trackit.App.Views.User;

public partial class UserDetailView
{
	public UserDetailView(UserDetailViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
	}
}