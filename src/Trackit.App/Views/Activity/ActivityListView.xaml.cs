using Trackit.App.ViewModels;
using Trackit.App.ViewModels.Activity;

namespace Trackit.App.Views.Activity;

public partial class ActivityListView
{
    private readonly ActivityListViewModel _viewModel;
    public ActivityListView(ActivityListViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
        _viewModel = viewModel;
    }
    private void AddActivityClicked(object sender, EventArgs e)
    {
    }

    private void OnBeforeDateCheckedChanged(object sender, EventArgs e)
    {
        _viewModel.IsBeforeDateSelected = ((Switch)sender).IsToggled;
        Task.Run(() => _viewModel.ReloadDataAsync("Before"));
    }

    private void OnAfterDateCheckedChanged(object sender, EventArgs e)
    {
        _viewModel.IsAfterDateSelected = ((Switch)sender).IsToggled;
        Task.Run(() => _viewModel.ReloadDataAsync("After"));
    }

    private void OnChecked(object sender, EventArgs e)
    {
        _ = Task.Run(() => _viewModel.ReloadDataAsync(((RadioButton)sender).Value.ToString()));
    }
}