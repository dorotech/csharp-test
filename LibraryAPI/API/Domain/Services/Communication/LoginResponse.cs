using LibraryApi.Domain.Entities;
using LibraryApi.Models;

namespace LibraryApi.Domain.Services.Communication
{
    public class LoginResponse : BaseResponse
    {
        public JwtModel Token { get; private set; }

        private LoginResponse(bool success, string message, JwtModel token) : base(success, message)
        {
            Token = token;
        }

        public LoginResponse(JwtModel token) : this(true, string.Empty, token) { }

        public LoginResponse(string message) : this(false, message, null) { }
    }
}