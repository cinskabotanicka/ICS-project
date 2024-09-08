using Trackit.App.ViewModels.User;

namespace Trackit.App.Views.User;

public partial class UserEditView 
{
    public UserEditView(UserEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}