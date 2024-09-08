using CommunityToolkit.Maui.Converters;
using System.Globalization;
using Trackit.BL.Models;

namespace Trackit.App.Converters;

public class ProjectToProjectNameConverter : BaseConverterOneWay<ProjectListModel, string>
{
    public override string ConvertFrom(ProjectListModel value, CultureInfo? culture)
        => value.Name;

    public override string DefaultConvertReturnValue { get; set; } = string.Empty;
}