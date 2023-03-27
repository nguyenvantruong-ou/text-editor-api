using TextEditor.Domain.Accounts.Entities;

namespace TextEditor.API.DTOs.ResponseDTOs
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string CardId { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime? DateCreated { get; set; }

        public LoginResponseDTO(Account acc, string token) 
        { 
            Id = acc.Id;
            Token = token;
            Name = acc.Name;
            CardId = acc.IdCard;
            Role = acc.Role.Name;
            Address = acc.Address;
            Gender = acc.Gender;
            DateCreated = acc.DateCreated;
        }
    }
}
