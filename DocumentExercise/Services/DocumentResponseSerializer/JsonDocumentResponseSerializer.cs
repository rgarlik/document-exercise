using System.Text.Json;
using System.Text.Json.Serialization;
using DocumentExercise.Model;

namespace DocumentExercise.Services.DocumentResponseSerializer;

/// <summary>
/// Serializes the document object into a JSON format
/// </summary>
public class JsonDocumentResponseSerializer : IDocumentResponseSerializer
{
    public string Serialize(Document document)
    {
        var jsonDocument = JsonSerializer.Serialize(document,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase } // we want camelCase property names
        );
        return jsonDocument;
    }
}