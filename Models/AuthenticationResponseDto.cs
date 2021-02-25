using System.Collections.Generic;

namespace Models
{
    public class AuthenticationResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
    }
}