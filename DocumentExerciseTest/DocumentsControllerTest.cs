using System.Text.Json;
using DocumentExercise.Controllers;
using DocumentExercise.Model;
using DocumentExercise.Services.DocumentResponseSerializer;
using DocumentExercise.Services.DocumentStorage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DocumentExerciseTest;

[TestFixture]
public class DocumentsControllerTest
{
    private DocumentsController _controller;
    private IDocumentStorage _storage;
    private IDocumentResponseSerializer _serializer;
    private ILogger<DocumentsController> _logger;
    private IMemoryCache _cache;
    
    [SetUp]
    public void SetUp()
    {
        _storage = new MemoryDocumentStorage();
        _serializer = new JsonDocumentResponseSerializer();
        _logger = new Logger<DocumentsController>(new LoggerFactory());
        _cache = new MemoryCache(new MemoryCacheOptions());
        _controller = new DocumentsController(_storage, _serializer, _logger, _cache);
    }

    /// <summary>
    /// Tests the creation of a new document
    /// </summary>
    [Test]
    public void TestCreateDocument()
    {
        dynamic data = new System.Dynamic.ExpandoObject();
        data.Field = "value";
        Document d = new Document{ Id = "1", Tags = new List<string>(){"tag1", "tag2"}, Data = data};
        
        IActionResult postResult = _controller.Post(d);
        Assert.IsInstanceOf(typeof(NoContentResult), postResult);

        var result = _controller.Get("1");
        var resultContent = (result as OkObjectResult).Value;
        var resultStringContent = resultContent != null ? (string) resultContent : string.Empty;

        Document? reselectedDoc = JsonSerializer.Deserialize<Document>(resultStringContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.IsNotNull(reselectedDoc);
        Assert.That(reselectedDoc?.Id, Is.EqualTo(d.Id));
        Assert.That(reselectedDoc?.Tags, Is.EqualTo(d.Tags));
        
        var dataJsonElement = (JsonElement)reselectedDoc?.Data;
        var dataJsonElementField = dataJsonElement.GetProperty("Field");
        Assert.IsTrue(dataJsonElementField.ValueEquals("value"));
    }

    /// <summary>
    /// Tests the behaviour when the user requests a non-existent document
    /// </summary>
    [Test]
    public void TestGetNonExistentDocument()
    {
        IActionResult getResult = _controller.Get("invalid id");
        Assert.IsInstanceOf(typeof(NotFoundResult), getResult);
    }
    
    /// <summary>
    /// Tests the behaviour when the user updates a non-existent document
    /// </summary>
    [Test]
    public void TestUpdateNonExistentDocument()
    {
        dynamic data = new System.Dynamic.ExpandoObject();
        data.Field = "value";
        Document d = new Document{ Id = "invalid id", Tags = new List<string>(){"tag1", "tag2"}, Data = data};
        
        IActionResult putResult = _controller.Put(d);
        Assert.IsInstanceOf(typeof(NotFoundResult), putResult);
    }
}