# MockH

[![CI](https://github.com/Kaliumhexacyanoferrat/MockH/actions/workflows/ci.yml/badge.svg)](https://github.com/Kaliumhexacyanoferrat/MockH/actions/workflows/ci.yml) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Kaliumhexacyanoferrat_MockH&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Kaliumhexacyanoferrat_MockH) [![nuget Package](https://img.shields.io/nuget/v/MockH.svg)](https://www.nuget.org/packages/MockH/)

This library allows to mock HTTP responses for integration, component and acceptance tests of your projects written in C# / .NET 6/7/8 by hosting a webserver returning configured responses.

- Fast and thread safe
- Only a few dependencies
- No configuration needed
- Does not interfer with Kestrel or ASP.NET
- Independent from the testing framework in place

## Usage

```csharp
using MockH;

[TestMethod]
public async Task TestSomething() 
{
   using var server = MockServer.Run
   (
       On.Get("/users/1").Return(new User(...)),
       On.Get("/users/2").Respond(ResponseStatus.NoContent)
   );

   // access the server in your code via HTTP
   using var client = new HttpClient();

   await client.GetStringAsync(server.Url("/users/1"));
}
```

## Basic Usage

```csharp
// return a specific status code
On.Get("/ifail").Respond(ResponseStatus.InternalServerError);

// redirect the client
On.Get().Redirect("https://github.com");

// execute logic and return some simple text value
On.Get().Run(() => "42");

// execute logic and return some JSON
private record MyClass(int IntValue, string StringValue);

On.Get().Run(() => new MyClass(42, "The answer"));

// execute logic asynchronously
On.Get().Run(async () => await ...);

// access query parameters (GET /increment?=1)
On.Get("/increment").Run((int i) => i + 1);

// access path parameters (GET /increment/1)
On.Get("/increment/:i").Run((int i) => i + 1);

// access request body
On.Post().Run((MyClass body) => body);

// access request body as stream
On.Post().Run((Stream body) => body.Length);
```

## Advanced Usage

```csharp
// directly access request and response
On.Get().Run((IRequest request) => request.Respond().Status(ResponseStatus.BadRequest));

// return a handler provided by the GenHTTP framework, e.g. a website
// see https://genhttp.org/documentation/content/
// can be useful if you want to test some kind of website crawler
On.Get().Run(() => Listing.From(ResourceTree.FromDirectory("/var/www")));
```
