using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Models
{
    public class LoginResult
    {
        public bool Success { get; }

        public string Token { get; }

        public string ErrorMessage { get; }

        public LoginResult(bool success, string token = null, string errorMessage = null)
        {
            Success = success;
            Token = token;
            ErrorMessage = errorMessage;
        }
    }
}
