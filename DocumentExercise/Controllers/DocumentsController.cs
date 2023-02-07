using DocumentExercise.Model;
using DocumentExercise.Services.DocumentResponseSerializer;
using DocumentExercise.Services.DocumentStorage;
using DocumentExercise.Services.DocumentStorage.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DocumentExercise.Controllers;

/// <summary>
/// The `/documents` endpoint controller
/// </summary>
[ApiController]
[Route("documents")]
public class DocumentsController : ControllerBase
{
    #region [ Services ]
    
    private readonly IDocumentStorage _storage;
    private readonly IDocumentResponseSerializer _serializer;
    private readonly ILogger<DocumentsController> _logger;
    private readonly IMemoryCache _cache;
    
    #endregion
    public DocumentsController(IDocumentStorage storage, IDocumentResponseSerializer serializer, ILogger<DocumentsController> logger, IMemoryCache cache)
    {
        _storage = storage;
        _serializer = serializer;
        _logger = logger;
        _cache = cache;
    }

    /// <summary>
    /// Gets a document by its ID
    /// </summary>
    /// <response code="200">Document found</response>
    /// <response code="404">Document not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        string responseText;

        // Get the document
        Document document;
        
        // Check if the document is in cache already
        if (!_cache.TryGetValue(id, out document))
        {
            // Get it from storage
            try
            {
                document = _storage.Read(id);
            }
            catch (DocumentNotFoundException)
            {
                return NotFound();
            }

            // Cache result
            _cache.Set(id, document);
        }

        // Serialize response
        try
        {
            responseText = _serializer.Serialize(document);
        }
        catch (Exception e)
        {
            _logger.LogError("Error serializing: " + e.ToString());
            return StatusCode(500);
        }
        
        // Return response
        return Ok(responseText);
    }
    
    /// <summary>
    /// Create a new Document
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /documents
    ///     {
    ///        "id": "some-unique-identifier1",
    ///        "tags": ["important", ".net"],
    ///        "data": {
    ///            "some": "data",
    ///            "optional": "fields"
    ///        }
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Document created</response>
    /// <response code="400">Document already exists</response>
    [HttpPost]
    public IActionResult Post([FromBody] Document document)
    {
        // Write into storage
        try
        {
            _storage.Write(document);
        }
        catch (DocumentAlreadyExists)
        {
            return BadRequest();
        }

        return NoContent();
    }

    /// <summary>
    /// Updates an already existing document
    /// </summary>
    /// <response code="204">Document updated</response>
    /// <response code="404">Document not found (needs to be created first)</response>
    [HttpPut]
    public IActionResult Put([FromBody] Document document)
    {
        // Update in storage
        try
        {
            _storage.Update(document);
        }
        catch (DocumentNotFoundException)
        {
            return NotFound();
        }

        // Remove from cache
        _cache.Remove(document.Id);
        
        return StatusCode(204);
    }
}