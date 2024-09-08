using Trackit.App.ViewModels.Project;

namespace Trackit.App.Views.Project;

public partial class ProjectListView
{
    public ProjectListView(ProjectListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
    private void AddProjectClicked(object sender, EventArgs e)
    {
    }

}