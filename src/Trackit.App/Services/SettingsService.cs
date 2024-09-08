using Trackit.App.Services.Interfaces;

namespace Trackit.App.Services;

public class SettingsService : ISettingsService
{
    IPreferences settings;

    public SettingsService(IPreferences settings)
    {
        this.settings = settings;
    }

    public Guid UserId
    {
        get => settings.Get("UserId", Guid.Empty);
        set => settings.Set("UserId", value);
    }
}