namespace PizzaApp.Shared.Responses
{
    public class LoginUserResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ValidTo { get; set; }
    }
}