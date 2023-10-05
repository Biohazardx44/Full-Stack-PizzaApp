namespace PizzaApp.Shared.CustomExceptions.ServerExceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException() : base("An error has occurred, contact the administrator!") { }
    }
}