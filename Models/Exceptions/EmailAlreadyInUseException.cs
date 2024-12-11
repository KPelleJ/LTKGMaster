namespace LTKGMaster.Models.Exceptions
{
    public class EmailAlreadyInUseException:Exception
    {
        public EmailAlreadyInUseException()
            : base("The email address is already in use.") { }

        public EmailAlreadyInUseException(string message)
            : base(message) { }

        public EmailAlreadyInUseException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
