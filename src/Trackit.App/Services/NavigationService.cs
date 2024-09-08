using Trackit.App.Models;
using Trackit.App.Services.Interfaces;
using Trackit.App.ViewModels.Activity;
using Trackit.App.ViewModels.Project;
using Trackit.App.ViewModels.User;
using Trackit.App.ViewModels;
using Trackit.App.Views.Project;
using Trackit.App.Views.User;
using Trackit.App.Views.Activity;


namespace Trackit.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UserListView), typeof(UserListViewModel)),
        new("//users/detail", typeof(UserDetailView), typeof(UserDetailViewModel)),

        new("//users/edit", typeof(UserEditView), typeof(UserEditViewModel)),
        new("//users/detail/edit", typeof(UserEditView), typeof(UserEditViewModel)),

        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),

        new("//activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        new("//activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),

        new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
        new("//projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),

        new("//projects/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),
        new("//projects/detail/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}