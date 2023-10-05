namespace PizzaApp.Shared.CustomExceptions.PizzaExceptions
{
    public class PizzaDataException : Exception
    {
        public PizzaDataException(string message) : base(message) { }
    }
}