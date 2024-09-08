﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackit.App.Messages;
using Trackit.App.Resources.Texts;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;

namespace Trackit.App.ViewModels.User;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserDetailViewModel : ViewModelBase, IRecipient<UserEditMessage>
{
    private readonly IUserFacade _UserFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; private set; }

    public UserDetailViewModel(
        IUserFacade UserFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _UserFacade = UserFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        User = await _UserFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (User is not null)
        {
            try
            {
                await _UserFacade.DeleteAsync(User.Id);
                MessengerService.Send(new UserDeleteMessage());
                _navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await _alertService.DisplayAsync(UserDetailViewModelTexts.DeleteError_Alert_Title, UserDetailViewModelTexts.DeleteError_Alert_Message);
            }
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await _navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(UserEditViewModel.User)] = User });
    }

    public async void Receive(UserEditMessage message)
    {
        if (message.UserId == User?.Id)
        {
            await LoadDataAsync();
        }
    }
}