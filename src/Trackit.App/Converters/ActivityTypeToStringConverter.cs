using CommunityToolkit.Maui.Converters;
using Trackit.Common.Enums;
using System.Globalization;
using Trackit.App.Resources.Texts;

namespace Trackit.App.Converters;

public class ActivityTypeToStringConverter : BaseConverterOneWay<ActivityType, string>
{
    public override string ConvertFrom(ActivityType value, CultureInfo? culture)
        => ActivityTypeTexts.ResourceManager.GetString(value.ToString(), culture)
           ?? ActivityTypeTexts.None;

    public override string DefaultConvertReturnValue { get; set; } = ActivityTypeTexts.None;
}