namespace PizzaApp.Shared.CustomExceptions.OrderExceptions
{
    public class OrderDataException : Exception
    {
        public OrderDataException(string message) : base(message) { }
    }
}