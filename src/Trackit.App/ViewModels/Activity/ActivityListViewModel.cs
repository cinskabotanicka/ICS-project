using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackit.App.Messages;
using Trackit.App.Services.Interfaces;
using Trackit.BL.Facades.Interfaces;
using Trackit.BL.Models;

namespace Trackit.App.ViewModels.Activity;

public partial class ActivityListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>, IRecipient<ProjectDeleteMessage>
{
    private readonly IActivityFacade _ActivityFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    private DateTime? _selectedAfterDate = DateTime.Now;
    public DateTime? SelectedAfterDate
    {
        get => _selectedAfterDate;
        set => SetProperty(ref _selectedAfterDate, value);
    }

    private DateTime? _selectedBeforeDate = DateTime.Now;
    public DateTime? SelectedBeforeDate
    {
        get => _selectedBeforeDate;
        set => SetProperty(ref _selectedBeforeDate, value);
    }

    private bool _isBeforeDateSelected;
    public bool IsBeforeDateSelected
    {
        get => _isBeforeDateSelected;
        set => SetProperty(ref _isBeforeDateSelected, value);
    }

    private bool _isAfterDateSelected;
    public bool IsAfterDateSelected
    {
        get => _isAfterDateSelected;
        set => SetProperty(ref _isAfterDateSelected, value);
    }

    public ActivityListViewModel(
        IActivityFacade ActivityFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _ActivityFacade = ActivityFacade;
        _navigationService = navigationService;

        IsAfterDateSelected = false;
        IsBeforeDateSelected = false;
    }

    public void ReloadDataAsync(string? filter)
    {
        Guid userId = Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000"));

        switch(filter)
        {
            case "Before":
            case "After":
            {
                    var filteredActivities = _ActivityFacade.GetByUserId(userId);

                    if (IsAfterDateSelected)
                    {
                        filteredActivities = filteredActivities.Where(activity => activity.Start >= SelectedAfterDate!.Value);
                    }

                    if (IsBeforeDateSelected)
                    {
                        filteredActivities = filteredActivities.Where(activity => activity.End <= SelectedBeforeDate!.Value);
                    }

                    Activities = filteredActivities;
                    break;
                }
            case "Week":
                Activities = _ActivityFacade.GetByUserIdDateRestrict(userId, DateTime.Now.AddDays(-7));
                break;
            case "Month":
                Activities = _ActivityFacade.GetByUserIdDateRestrict(userId, DateTime.Now.AddMonths(-1));
                break;
            case "Ninety":
                Activities = _ActivityFacade.GetByUserIdDateRestrict(userId, DateTime.Now.AddDays(-90));
                break;
            case "Year":
                Activities = _ActivityFacade.GetByUserIdDateRestrict(userId, DateTime.Now.AddYears(-1));
                break;
            default:
                Activities = _ActivityFacade.GetByUserId(userId);
                break;
        }
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Guid userId = Guid.Parse(Preferences.Default.Get("usr", "00000000-0000-0000-0000-000000000000"));

        Activities = _ActivityFacade.GetByUserId(userId);
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await _navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}