using DocumentExercise.Model;

namespace DocumentExercise.Services.DocumentResponseSerializer;

/// <summary>
/// This service serializes a Document object into a string to be sent
/// back to the client as a response
/// </summary>
public interface IDocumentResponseSerializer
{
    /// <summary>
    /// Serializes the provided document to be sent back
    /// to the client
    /// </summary>
    /// <param name="document">Document object</param>
    /// <returns>Serialized string</returns>
    public string Serialize(Document document);
}