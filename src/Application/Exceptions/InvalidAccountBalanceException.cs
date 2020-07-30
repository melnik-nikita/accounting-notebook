namespace Application.Exceptions
{
    public class InvalidAccountBalanceException : ValidationException
    {
        public InvalidAccountBalanceException() { }

        public InvalidAccountBalanceException(string message)
            : base(message)
        {
        }
    }
}
