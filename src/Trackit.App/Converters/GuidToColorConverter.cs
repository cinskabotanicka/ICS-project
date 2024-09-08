using CommunityToolkit.Maui.Converters;
using System.Globalization;
using System.Numerics;

namespace Trackit.App.Converters
{
    public class GuidToColorConverter : BaseConverterOneWay<Guid, Color>
    {
        public override Color ConvertFrom(Guid value, CultureInfo? culture)
        {
            var str = value.ToString();
            BigInteger hash = 0;

            for (var i = 0; i < str.Length; i++)
            {
                hash = (int)str[i] + ((hash << 5) - hash);
            }
            var colour = "#FF";
            for (var i = 0; i < 3; i++)
            {
                var colValue = (hash >> (i * 8)) & 0xFF;
                var tmp = ("00" + ((int)colValue).ToString("X"));
                colour += tmp[^2..];
            }

            return Color.FromArgb(colour);
        }

        public override Color DefaultConvertReturnValue { get; set; } = Colors.Black;
    }
}