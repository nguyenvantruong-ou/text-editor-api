namespace TextEditor.API.DTOs.RequestDTOs
{
    public class RegisterRequestDTO
    {
        public string? IdCard { get; set; }
        public string? Password { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
    }
}
