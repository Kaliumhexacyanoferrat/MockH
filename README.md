# MockH

[![CI](https://github.com/Kaliumhexacyanoferrat/MockH/actions/workflows/ci.yml/badge.svg)](https://github.com/Kaliumhexacyanoferrat/MockH/actions/workflows/ci.yml) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Kaliumhexacyanoferrat_MockH&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Kaliumhexacyanoferrat_MockH) [![nuget Package](https://img.shields.io/nuget/v/MockH.svg)](https://www.nuget.org/packages/MockH/)

This library allows to mock HTTP webserver responses for integration and acceptance tests of projects written in C# / .NET. 

- Only a few dependencies
- Does not interfer with Kestrel or ASP.NET
- Independent from the testing framework

## Usage

```
using MockH;

[TestMethod]
public async Task TestSomething() 
{
   using var server = MockServer.Run
   (
       On.Get("/users/1").Return(new User(...))
   );

   // access the server in your code via HTTP
   using var client = new HttpClient();
   await client.GetStringAsync(server.Url("/users/1"));
}
```
