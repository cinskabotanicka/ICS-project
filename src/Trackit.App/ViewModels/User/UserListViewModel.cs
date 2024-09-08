using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackit.App.Messages;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Models;
using Trackit.BL.Facades.Interfaces;

namespace Trackit.App.ViewModels.User;

public partial class UserListViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _UserFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;

    public UserListViewModel(
        IUserFacade UserFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _UserFacade = UserFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Preferences.Default.Set("usr", Guid.Empty.ToString());

        Users = await _UserFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        Preferences.Default.Set("usr", id.ToString());

        MessengerService.Send(new ActivityDeleteMessage());

        await _navigationService.GoToAsync<UserDetailViewModel>(
            new Dictionary<string, object?> { [nameof(UserDetailViewModel.Id)] = id });
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}