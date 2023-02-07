namespace DocumentExercise.Services.DocumentStorage.Exceptions;

/// <summary>
/// Occurs when creating a document already exists
/// </summary>
public class DocumentAlreadyExists : Exception
{
    public DocumentAlreadyExists() { }

    public DocumentAlreadyExists(string message) : base(message) { }

    public DocumentAlreadyExists(string message, Exception inner) : base(message, inner) { }
}