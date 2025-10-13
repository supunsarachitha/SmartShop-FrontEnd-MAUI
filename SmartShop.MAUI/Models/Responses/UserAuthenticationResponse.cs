using SmartShop.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.MAUI.Models.Responses
{
    public class UserAuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public User? User { get; set; }
        public string? Token { get; set; }
    }
}