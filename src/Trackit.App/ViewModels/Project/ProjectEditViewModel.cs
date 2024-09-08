using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.Tokens;
using Trackit.App.Messages;
using Trackit.App.Resources.Texts;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;

namespace Trackit.App.ViewModels.Project;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ProjectUserEditMessage>, IRecipient<ProjectUserAddMessage>, IRecipient<ProjectUserDeleteMessage>
{
    private readonly IProjectFacade _ProjectFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;

    public ProjectEditViewModel(
        IProjectFacade ProjectFacade,
        INavigationService navigationService,
        IAlertService alertService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _ProjectFacade = ProjectFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Project.Name.IsNullOrEmpty())
        {
            await _alertService.DisplayAsync(ProjectEditViewModelTexts.Missing_Fields_Title, ProjectEditViewModelTexts.Missing_Fields_Message_Name);
            return;
        }
        await _ProjectFacade.SaveAsync(Project with { Users = default! });

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

        _navigationService.SendBackButtonPressed();
    }

    public async void Receive(ProjectUserEditMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(ProjectUserAddMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(ProjectUserDeleteMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        Project = await _ProjectFacade.GetAsync(Project.Id)
                 ?? ProjectDetailModel.Empty;
    }
}