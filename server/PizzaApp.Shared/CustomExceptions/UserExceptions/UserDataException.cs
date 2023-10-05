namespace PizzaApp.Shared.CustomExceptions.UserExceptions
{
    public class UserDataException : Exception
    {
        public UserDataException(string message) : base(message) { }
    }
}