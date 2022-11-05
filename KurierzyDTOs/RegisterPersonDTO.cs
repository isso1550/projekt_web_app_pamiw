namespace KurierzyDTOs
{
    public class RegisterPersonDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string? City { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }

}
