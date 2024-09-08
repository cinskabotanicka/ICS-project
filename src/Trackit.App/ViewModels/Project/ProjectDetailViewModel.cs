using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.CodeDom;
using Trackit.App.Messages;
using Trackit.App.Resources.Texts;
using Trackit.App.Services;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;

namespace Trackit.App.ViewModels.Project;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectDetailViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectUserAddMessage>, IRecipient<ProjectUserDeleteMessage>
{
    private readonly IProjectFacade _ProjectFacade;
    private readonly IActivityFacade _ActivityFacade;
    private readonly IUserFacade _UserFacade;
    private readonly IUsersInProjectFacade _UsersInProjectFacade;
    private readonly INavigationService _navigationService;

    public Guid Id { get; set; }
    public ProjectDetailModel? Project { get; set; }

    public IEnumerable<ActivityListModel>? Activities { get; set; }
    public IEnumerable<UserListModel>? Users { get; set; }

    public ProjectDetailViewModel(
        IProjectFacade ProjectFacade,
        IUsersInProjectFacade UsersInProjectFacade,
        IActivityFacade ActivityFacade,
        IUserFacade UserFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _ProjectFacade = ProjectFacade;
        _ActivityFacade = ActivityFacade;
        _UserFacade = UserFacade;
        _navigationService = navigationService;
        _UsersInProjectFacade = UsersInProjectFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _ProjectFacade.GetAsync(Id);
        Activities = _ActivityFacade.GetByProjectId(Project!.Id);
        Users = _UserFacade.GetByIdArray(Project.Users.ToList());
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Project is not null)
        {
            await _ProjectFacade.DeleteAsync(Project.Id);

            MessengerService.Send(new ProjectDeleteMessage());

            _navigationService.SendBackButtonPressed();
        }
    }


    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Project is not null)
        {
            await _navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(ProjectEditViewModel.Project)] = Project with { } });
        }
    }

    [RelayCommand]
    private async Task JoinLeaveAsync()
    {
        var currUser = Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000"));
        var joined = (Users ?? Array.Empty<UserListModel>()).Any(u => u.Id == currUser);

        if (!joined)
            await _UsersInProjectFacade.SaveAsync(new UsersInProjectDetailModel { ProjectId = Project!.Id, UserId = currUser }, Project.Id);
        else
            _UsersInProjectFacade.DeleteByUserAndProject(Project!.Id, currUser);

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });
    }

    public async void Receive(ProjectEditMessage message)
    {
        if (message.ProjectId == Project?.Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(ProjectUserAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectUserDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public string JoinLeaveText
    {
        get
        {
            if (Users == null)
                return "Toggle Membership";
            var currUser = Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000"));
            return Users.Any(u => u.Id == currUser) ? ProjectDetailViewModelTexts.LeaveProject_Text : ProjectDetailViewModelTexts.JoinProject_Text;
        }
    }

    public bool UserLogged => Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000")) != Guid.Empty;
}