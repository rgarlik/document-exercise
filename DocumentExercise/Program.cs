using System.Reflection;
using DocumentExercise.Services.DocumentResponseSerializer;
using DocumentExercise.Services.DocumentStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        // Include XML docs from the controllers using reflection magic
        // and turn them into Swagger docs
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
);

// Add custom services
builder.Services.AddSingleton<IDocumentStorage, MemoryDocumentStorage>();

builder.Services.AddSingleton<IDocumentResponseSerializer, JsonDocumentResponseSerializer>();
// try this for a different serializer
// builder.Services.AddSingleton<IDocumentResponseSerializer, XmlDocumentResponseSerializer>();

// Add caching
builder.Services.AddMemoryCache();

var app = builder.Build();

// Swagger UI for development purposes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();