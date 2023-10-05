namespace PizzaApp.Shared.CustomExceptions.UserExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}