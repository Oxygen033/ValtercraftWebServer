namespace ValtercraftWebServer.DTO
{
    public class WhiteListRequestDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public UserDto User { get; set; }
    }
}
