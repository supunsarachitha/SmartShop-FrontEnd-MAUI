using Microsoft.Maui.Storage;
using System.Text.Json;

namespace SmartShop.MAUI.Helpers
{
    public static class PreferenceHelper
    {
        /// <summary>
        /// Saves a value to preferences.
        /// </summary>
        /// <typeparam name="T">The type of the value to save.</typeparam>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to save.</param>
        public static void SetPreference<T>(string key, T value)
        {
            if (value == null)
            {
                Preferences.Remove(key);
                return;
            }

            if (value is string stringValue)
            {
                Preferences.Set(key, stringValue);
            }
            else
            {
                string json = JsonSerializer.Serialize(value);
                Preferences.Set(key, json);
            }
        }

        /// <summary>
        /// Retrieves a value from preferences.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The retrieved value, or the default value if the key does not exist.</returns>
        public static T? GetPreference<T>(string key, T? defaultValue = default)
        {
            if (typeof(T) == typeof(string))
            {
                return (T?)(object?)Preferences.Get(key, defaultValue as string);
            }

            string? json = Preferences.Get(key, null);
            if (string.IsNullOrEmpty(json))
            {
                return defaultValue;
            }

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}