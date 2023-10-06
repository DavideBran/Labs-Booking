public class InvalidHourException : Exception
{
    public InvalidHourException() { }
}

public class InvalidLabException : Exception
{
    public InvalidLabException() { }

    public InvalidLabException(string message) : base(message) { }
}

public class InvalidDayException : Exception
{
    public InvalidDayException() { }
}