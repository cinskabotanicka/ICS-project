using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using Trackit.App.Messages;
using Trackit.App.Resources.Texts;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;
using Trackit.Common.Enums;

namespace Trackit.App.ViewModels.Activity;

[QueryProperty(nameof(Activity), nameof(Activity))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;
    private IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public List<ProjectListModel> Projects { get; set; } = null!;
    public ProjectListModel Project { get; set; } = ProjectListModel.Empty;

    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
    public List<ActivityType> ActivityTypes { get; set; }

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _alertService = alertService;

        if (Activity.Start == DateTime.MinValue && Activity.End == DateTime.MaxValue)
        {
            Activity.Start = DateTime.Now;
            Activity.End = DateTime.Now.AddMinutes(1);
        }

        ActivityTypes = new List<ActivityType>((ActivityType[])Enum.GetValues(typeof(ActivityType)));
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = _projectFacade.GetByUserId(Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000"))).ToList();
        var fetched = _projectFacade.GetById(Activity.ProjectId);
        Project = fetched.Count() > 0 ? fetched.First() : ProjectListModel.Empty;
        OnPropertyChanged(nameof(SelectedProject));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!await CheckValidity()) return;

        Activities = _activityFacade.GetByUserId(Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000")));
        Activities = Activities.Where(a => Activity.Start <= a.End && Activity.End >= a.Start && a.Id != Activity.Id);

        if (Activities.Count() != 0)
        {
            await _alertService.DisplayAsync(ActivityEditViewModelTexts.OverlapError_Alert_Title, ActivityEditViewModelTexts.OverlapError_Alert_Message);
            return;
        }

        if (Activity.UserId == Guid.Empty)
            Activity.UserId = Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000"));

        if (Activity.Id == Guid.Empty)
            Activity.Id = Guid.NewGuid();

        Activity.ProjectId = Project.Id;

        await _activityFacade.SaveAsync(Activity);

        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

        _navigationService.SendBackButtonPressed();
    }

    public DateTime SelectedStartDate
    {
        get => Activity.Start.Date;
        set
        {
            if (Activity.Start.Date == value) return;
            Activity.Start = value + Activity.Start.TimeOfDay;
            OnPropertyChanged(nameof(SelectedStartDate));
        }
    }
    public TimeSpan SelectedStartTime
    {
        get => Activity.Start.TimeOfDay;
        set
        {
            if (Activity.Start.TimeOfDay == value) return;
            Activity.Start = Activity.Start.Date.Add(value);
            OnPropertyChanged(nameof(SelectedStartTime));
        }
    }

    public DateTime SelectedEndDate
    {
        get => Activity.End.Date;
        set
        {
            if (Activity.End.Date == value) return;
            Activity.End = value + Activity.End.TimeOfDay;
            OnPropertyChanged(nameof(SelectedEndDate));
        }
    }
    public TimeSpan SelectedEndTime
    {
        get => Activity.End.TimeOfDay;
        set
        {
            if (Activity.End.TimeOfDay == value) return;
            Activity.End = Activity.End.Date.Add(value);
            OnPropertyChanged(nameof(SelectedEndTime));
        }
    }

    public ProjectListModel SelectedProject
    {
        get
        {
            if (Projects == null || Project == ProjectListModel.Empty || Project == null)
                return ProjectListModel.Empty;
            return Projects.Find(p => p.Id == Project.Id)!;
        }
        set
        {
            if (Project == value) return;
            Project = value;
            OnPropertyChanged(nameof(SelectedProject));
        }
    }

    private async Task<bool> CheckValidity()
    {
        if (Activity.Name.IsNullOrEmpty())
        {
            await _alertService.DisplayAsync(ActivityEditViewModelTexts.Missing_Fields_Title, ActivityEditViewModelTexts.Missing_Fields_Message_Name);
            return false;
        }

        if (Project.Id == Guid.Empty)
        {
            await _alertService.DisplayAsync(ActivityEditViewModelTexts.Missing_Fields_Title, ActivityEditViewModelTexts.Missing_Fields_Message_Project);
            return false;
        }

        if (Activity.Start >= Activity.End)
        {
            await _alertService.DisplayAsync(ActivityEditViewModelTexts.Invalid_Date_Title, ActivityEditViewModelTexts.Invalid_Date_Message);
            return false;
        }

        return true;
    }
}