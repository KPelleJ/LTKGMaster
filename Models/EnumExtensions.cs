using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LTKGMaster.Models
{
    /// <summary>
    /// Denne klasse er en extension til vores enum som bare viser et specifikt navn ude på websiten som vi så bestemmer i enumklassen
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Denne metode giver os muligheden for at DisplayName virker ude på vores html sider ellers kom navnet på den enkelte
        /// enum bare frem
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }
    }
}
