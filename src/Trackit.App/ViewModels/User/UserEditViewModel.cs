using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using Trackit.App.Messages;
using Trackit.App.Resources.Texts;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;

namespace Trackit.App.ViewModels.User;

[QueryProperty(nameof(User), nameof(User))]
public partial class UserEditViewModel : ViewModelBase
{
    private readonly IUserFacade _UserFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public UserDetailModel User { get; init; } = UserDetailModel.Empty;

    public UserEditViewModel(
        IUserFacade UserFacade,
        INavigationService navigationService,
        IAlertService alertService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _UserFacade = UserFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (User.FirstName.IsNullOrEmpty())
        {
            await _alertService.DisplayAsync(UserEditViewModelTexts.Missing_Fields_Title, UserEditViewModelTexts.Missing_Fields_Message_FirstName);
            return;
        }

        if (User.LastName.IsNullOrEmpty())
        {
            await _alertService.DisplayAsync(UserEditViewModelTexts.Missing_Fields_Title, UserEditViewModelTexts.Missing_Fields_Message_LastName);
            return;
        }

        await _UserFacade.SaveAsync(User);

        MessengerService.Send(new UserEditMessage { UserId = User.Id });

        _navigationService.SendBackButtonPressed();
    }
}