using DocumentExercise.Model;
using DocumentExercise.Services.DocumentStorage.Exceptions;

namespace DocumentExercise.Services.DocumentStorage;

/// <summary>
/// The DocumentStorage service contains definitions for writing
/// and reading documents from any underlying storage.
/// </summary>
public interface IDocumentStorage
{
    /// <summary>
    /// Add a document into the storage
    /// Will throw an Exception if a document with that Id already exists.
    /// </summary>
    /// <param name="document">The document object to add into storage</param>
    /// <exception cref="DocumentAlreadyExists">A document with this Id already exists in storage</exception>
    public void Write(Document document);

    /// <summary>
    /// Update an existing document record
    /// </summary>
    /// <param name="document">The document object to update</param>
    /// <exception cref="DocumentNotFoundException">The updated document does not exist yet and needs to be added with Write() first</exception>
    public void Update(Document document);

    /// <summary>
    /// Obtain a document from storage by its Id
    /// </summary>
    /// <param name="id">Id of the document</param>
    /// <returns>The complete document object</returns>
    /// <exception cref="DocumentNotFoundException">No document with that Id exists in storage</exception>
    public Document Read(string id);
}