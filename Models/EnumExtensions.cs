using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LTKGMaster.Models
{
    /// <summary>
    /// This is the class the gives us the ability to display the name of our enums in our code that we decide in the enum class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// This is the method that gives us the oppotunity to Display the name of our enum annotation DisplayName out in the website.
        /// </summary>
        /// <param name="value">The enum form which we want to use the DisplayName.</param>
        /// <returns>Returns the string that shows in the Display name annotation.</returns>
        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }
    }
}
