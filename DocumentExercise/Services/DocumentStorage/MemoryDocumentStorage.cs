using DocumentExercise.Model;
using DocumentExercise.Services.DocumentStorage.Exceptions;

namespace DocumentExercise.Services.DocumentStorage;

/// <summary>
/// An in-memory implementation of the DocumentStorage service
/// </summary>
public class MemoryDocumentStorage : IDocumentStorage
{
 private Dictionary<string, Document> _storage = new Dictionary<string, Document>();
 
 public void Write(Document document)
  {
   if (_storage.ContainsKey(document.Id))
   {
    throw new DocumentAlreadyExists();
   }
   else
   {
    _storage.Add(document.Id, document);
   }
  }

  public void Update(Document document)
  {
   if (!_storage.ContainsKey(document.Id))
   {
    throw new DocumentNotFoundException();
   }
   else
   {
    _storage[document.Id] = document;
   }
  }

  public Document Read(string id)
  {
   if (!_storage.ContainsKey(id))
   {
    throw new DocumentNotFoundException();
   }
   else
   {
    return _storage[id];
   }
  }
}