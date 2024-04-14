namespace WebShop.Exceptions
{
    public class NotEnoughProductException : Exception
    {
        public NotEnoughProductException(string message) : base(message) { }
    }
}
