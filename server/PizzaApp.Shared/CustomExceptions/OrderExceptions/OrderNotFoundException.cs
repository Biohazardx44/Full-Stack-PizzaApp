namespace PizzaApp.Shared.CustomExceptions.OrderExceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message) : base(message) { }
    }
}