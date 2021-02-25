using System.Collections.Generic;

namespace Models
{
    public class RegisterResponseDto
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}