# LearningDIHub

## Overview

LearningDIHub is a playground to explore Dependency Injection patterns and behaviors using .NET (target: .NET 10). It contains small experiments that demonstrate service registration patterns, lifetimes, decorators, keyed/keyed-typed registrations, and strategies for handling circular dependencies.

## Projects in the solution

- LearningDIHub.Console — Console entrypoint and samples (appsettings.json included).
- LearningDIHub.CompositionRoot — Central composition helpers and a LearningHub hosted service.
- LearningDIHub.MessageSender — Message sending implementations, decorators, and registration helpers.
- LearningDIHub.Source (DataSource) — IMessageSource implementations: HttpMessageSource and DBMessageSource and typed HttpClient registration.
- LearningDIHub.Auditing — Generic audit logger and message audit logger registration.
- LearningDIHub.Circular — Examples of circular dependency handling using Lazy<T> and Func<T>.
- LearningDIHub.Domain — Domain models and configuration objects used across the solution.

## Concepts covered

- Constructor injection and service resolution
- Service lifetimes: Singleton, Scoped, Transient
- Multiple implementations & IEnumerable<T> injection
- ServiceDecorator (Scrutor) usage
- Options pattern and typed HttpClient
- Keyed/typed registrations for IMessageSource
- Circular dependency patterns with Lazy<T> and Func<T>
- Hosted service composition and IHostBuilder integration
- Generic auditing with IAuditLogger<T>

## Experiments and examples

- AddSimpleMessageServices / AddMessageServices: different registration strategies for IMessageService and ISenderProvider implementations.
- ServiceSelector options to choose a provider at runtime (configured via appsettings.json).
- MessageServiceLoggingDecorator demonstrates decorating a service to add cross-cutting concerns.
- DataSource demonstrates registering a typed HttpClient and keyed IMessageSource implementations ("http", "db").
- Circular shows how to resolve circular dependencies safely using Lazy<T> and Func<T>.
- CompositionRoot exposes AddLearningHub to wire up the full set of services and optionally register LearningHub as an IHostedService.

## Configuration (appsettings.json)

The Console project contains an appsettings.json with keys that the examples consume:

- MessageService:SelectedService — identifier used by ServiceSelector to enable a specific ISenderProvider.
- MessageSource:URI — base URI used by the typed HttpClient for the HttpMessageSource.

Example (LearningDIHub.Console/appsettings.json):

```json
{
  "MessageService:SelectedService": "1",
  "MessageSource:URI": "https://raw.githubusercontent.com/jprodriguezm90/LearningDIHub/refs/heads/master/"
}
```

## How to run

Restore and run the console sample (runs several DI experiments and then starts a hosted LearningHub):

1. Restore & build the solution

   ```bash
   dotnet restore
   dotnet build
   ```

2. Run the console project

   ```bash
   dotnet run --project LearningDIHub.Console
   ```

The console application demonstrates manual ServiceCollection usage, lifetime behavior, circular dependency experiments, and then builds an IHost that can register and run the LearningHub hosted service.

## Notes

- Target framework: .NET 10
- This repository is intended for learning and experimentation, not production usage.
- See source code for concrete examples and additional comments in registration helper classes (CompositionRoot, MessageServicesRegistration, DataServicesRegistration, Circular registrations, etc.).
