namespace userpanel.api.Exceptions;

public class InvalidUserCredentialsException : Exception
{
    public InvalidUserCredentialsException(string message) : base(message) { }
}