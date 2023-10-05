namespace PizzaApp.Shared.CustomExceptions.PizzaExceptions
{
    public class PizzaNotFoundException : Exception
    {
        public PizzaNotFoundException(string message) : base(message) { }
    }
}