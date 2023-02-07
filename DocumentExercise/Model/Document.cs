using System.Xml.Serialization;

namespace DocumentExercise.Model;

/// <summary>
/// Model of a document
/// </summary>
[Serializable]
public class Document
{
    /// <summary>
    /// Unique identifier of the document
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// List of document tags
    /// </summary>
    public List<string> Tags { get; set; }
    
    /// <summary>
    /// Dynamic document data
    /// </summary>
    public dynamic Data { get; set; }
}