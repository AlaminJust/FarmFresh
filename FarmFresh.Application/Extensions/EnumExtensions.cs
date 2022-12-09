using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets human-readable version of enum.
        /// </summary>
        /// <returns>effective DisplayAttribute.Name of given enum.</returns>
        public static string GetDisplayName<T>(this T enumValue) where T : IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Argument must be of type Enum");

            
            DisplayAttribute displayAttribute = enumValue.GetType()
                                                         .GetMember(enumValue.ToString())
                                                         .First()
                                                         .GetCustomAttribute<DisplayAttribute>();

            string displayName = displayAttribute?.GetName();

            return displayName ?? enumValue.ToString();
        }
    }
}
