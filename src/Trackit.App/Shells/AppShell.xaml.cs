using CommunityToolkit.Mvvm.Input;
using Trackit.App.Services.Interfaces;
using Trackit.App.ViewModels;
using Trackit.App.ViewModels.Activity;
using Trackit.App.ViewModels.Project;
using Trackit.App.ViewModels.User;

namespace Trackit.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToActivitiesAsync()
        => await _navigationService.GoToAsync<ActivityListViewModel>();

    [RelayCommand]
    private async Task GoToUsersAsync()
        => await _navigationService.GoToAsync<UserListViewModel>();

    [RelayCommand]
    private async Task GoToProjectsAsync()
    => await _navigationService.GoToAsync<ProjectListViewModel>();
}