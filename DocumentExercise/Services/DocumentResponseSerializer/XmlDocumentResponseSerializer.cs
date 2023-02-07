using System.Xml;
using System.Xml.Serialization;
using DocumentExercise.Model;
using Microsoft.OpenApi.Extensions;

namespace DocumentExercise.Services.DocumentResponseSerializer;

public class XmlDocumentResponseSerializer : IDocumentResponseSerializer
{
    private XmlSerializer _serializer;
    
    public XmlDocumentResponseSerializer()
    {
        _serializer = new XmlSerializer(typeof(Document), new [] { typeof(System.Text.Json.JsonElement) } );
    }

    public string Serialize(Document document)
    {
        string xmlDocument;
        using (var sw = new StringWriter())
        {
            using(XmlWriter writer = XmlWriter.Create(sw))
            {
                _serializer.Serialize(writer, document);
                xmlDocument = sw.ToString();
            }
        }

        return xmlDocument;
    }
}