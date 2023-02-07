namespace DocumentExercise.Services.DocumentStorage.Exceptions;

/// <summary>
/// Occurs when a document Id being looked up in the storage does not exist
/// </summary>
public class DocumentNotFoundException : Exception
{
    public DocumentNotFoundException() { }

    public DocumentNotFoundException(string message) : base(message) { }

    public DocumentNotFoundException(string message, Exception inner) : base(message, inner) { }
}