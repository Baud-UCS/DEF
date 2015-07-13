using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baud.Deployment.Resources
{
    /// <summary>
    /// BoolFormatProvider provides custom strings for True/False values.
    /// </summary>
    public class BoolFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            bool value = (bool)arg;
            format = format == null ? null : format.Trim().ToLower();

            switch (format)
            {
                case "yn":
                    return value ? StringResources.True : StringResources.False;
                default:
                    if (arg is IFormattable)
                    {
                        return ((IFormattable)arg).ToString(format, formatProvider);
                    }
                    else
                    {
                        return arg.ToString();
                    }
            }
        }
    }

    public static class BooleanExtensions
    {
        public static string ToString(this bool value, string format)
        {
            return string.Format(new BoolFormatProvider(), "{0:" + format + "}", value);
        }
    }
}
