namespace MoviesAPI.DTOs.User
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
