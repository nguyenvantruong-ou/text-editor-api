using System.ComponentModel.DataAnnotations;

namespace TextEditor.API.DTOs.RequestDTOs
{
    public class LoginRequestDTO
    {
        [MinLength(6)]
        public string Username { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
    }
}
