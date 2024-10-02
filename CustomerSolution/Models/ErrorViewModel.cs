namespace CustomerSolution.Models;

public class ErrorViewModel(Exception exception, bool showException = false)
{
    public Exception Exception { get; set; } = exception;

    public bool ShowException { get; set; } = showException;
}