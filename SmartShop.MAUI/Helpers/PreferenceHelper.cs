using Microsoft.Maui.Storage;
using System.Text.Json;

namespace SmartShop.MAUI.Helpers
{
    public static class PreferenceHelper
    {
        /// <summary>
        /// Saves a value to secure storage.
        /// </summary>
        /// <typeparam name="T">The type of the value to save.</typeparam>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to save.</param>
        public static async Task SetSecurePreferenceAsync<T>(string key, T value)
        {
            if (value == null)
            {
                SecureStorage.Remove(key);
                return;
            }

            if (value is string stringValue)
            {
                await SecureStorage.SetAsync(key, stringValue);
            }
            else
            {
                string json = JsonSerializer.Serialize(value);
                await SecureStorage.SetAsync(key, json);
            }
        }

        /// <summary>
        /// Retrieves a value from secure storage.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="defaultValue">The default value to return if the key does not exist.</param>
        /// <returns>The retrieved value, or the default value if the key does not exist.</returns>
        public static async Task<T?> GetSecurePreferenceAsync<T>(string key, T? defaultValue = default)
        {
            try
            {
                string? json = await SecureStorage.GetAsync(key);
                if (string.IsNullOrEmpty(json))
                {
                    return defaultValue;
                }

                if (typeof(T) == typeof(string))
                {
                    return (T?)(object?)json;
                }

                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                // Handle cases where SecureStorage is unavailable or the key doesn't exist
                return defaultValue;
            }
        }
    }
}