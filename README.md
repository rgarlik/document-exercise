# Document Exercise

A ASP.NET Core webapp with a `/documents` endpoint that allows storing documents of the following structure:
```json
{
    "id": "some-unique-identifier1",
    "tags": ["important", ".net"],
    "data": {
         "some": "data",
         "optional": "fields"
    }
}
```

Check the fully documented Swagger UI to see the possible endpoint methods and try it out (at `/swagger/index.html`).

The application is also easily extendable, either by adding new storage systems
(by implementing the `IDocumentStorage` service interface) or new serialization methods for the returned data
(by implementing the `IDocumentResponseSerializer` service interface).

By default, the documents are stored in-memory and the reutrned data can be either in JSON or XML format (depends
on the service implementation you choose for the builder)

In-memory caching is also present.
