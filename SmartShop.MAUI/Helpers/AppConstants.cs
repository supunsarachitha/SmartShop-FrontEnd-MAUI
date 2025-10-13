using SmartShop.MAUI.Models;
using SmartShop.MAUI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.MAUI.Helpers
{
    public static class AppConstants
    {
        public const string ApiBaseUrl = "http://localhost:5218";
        public static User CurrentUser = new User();
        public static string AuthToken = string.Empty;
    }
}
