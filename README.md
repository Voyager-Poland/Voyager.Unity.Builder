# Voyager.Unity.Builder

Voyager.Unity.Builder is a tool designed to integrate the **Microsoft.Extensions.DependencyInjection** (MS DI) dependency injection system with the Unity container, which is required in some OWIN-based solutions. This library allows the use of modern type registration features and lifecycle management of objects, including support for the `Scope` lifecycle in MS DI, while maintaining compatibility with `IUnityContainer`.

## Table of Contents

- [Requirements](#requirements)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Examples](#examples)
  - [Example 1: Using UnityBuilder with ServiceCollection Registration](#example-1-using-unitybuilder-with-servicecollection-registration)
  - [Example 2: Managing Objects in Scope](#example-2-managing-objects-in-scope)
- [Support](#support)
- [License](#license)

## Requirements

To use **Voyager.Unity.Builder**, you will need:

- .NET Framework or .NET Core (compatible version for your project)
- Unity (for projects using `IUnityContainer`)
- NuGet package management (or `dotnet CLI`)

## Installation

To install the **Voyager.Unity.Builder** package, you can use the .NET CLI or NuGet Package Manager in Visual Studio.

### Using .NET CLI:

```bash
dotnet add package Voyager.Unity.Builder
```

### Using NuGet Package Manager:

1. In Visual Studio, right-click on your project in **Solution Explorer**.
2. Choose **Manage NuGet Packages**.
3. Search for `Voyager.Unity.Builder` in the **Browse** tab.
4. Click **Install**.

Once installed, the package will be added to your project and available for use.

## Configuration

Voyager.Unity.Builder allows you to register types in a `ServiceCollection` and then create an `IUnityContainer`, which is especially useful in OWIN-based projects. This provides the benefit of using modern MS DI registration and lifecycle management.

### Main Methods:

- **`CreateBuilder(IServiceCollection services)`**: Creates an `IUnityContainer` based on the provided `IServiceCollection`.
- **`CreateServiceProvider(IUnityContainer container)`**: Creates an `IServiceProvider` based on the Unity container, enabling the use of MS DI.

## Usage

### Registering and Using Unity Container with `ServiceCollection`:

- Register services in the `IServiceCollection` using methods such as `AddSingleton()`, `AddScoped()`, and `AddTransient()`.
- Use the `CreateBuilder` method to initialize an `IUnityContainer` with the registered services.
- Create an `IServiceProvider` to use the registered services in a modern DI style.

## Examples

### Example 1: Using UnityBuilder with ServiceCollection Registration

In this example, we'll show how to use the `UnityBuilder` class to create an `IUnityContainer` using services registered in a `ServiceCollection`.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Voyager.Unity.Builder;
using Unity;

class Program
{
    static void Main(string[] args)
    {
        // Create a ServiceCollection and register services
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IMyService, MyService>();
        serviceCollection.AddTransient<IMyFactory, MyFactory>();

        // Create an instance of UnityBuilder
        var unityBuilder = new UnityBuilder();

        // Create a Unity container based on the registered services
        var unityContainer = unityBuilder.CreateBuilder(serviceCollection);

        // Create an IServiceProvider from the Unity container
        var serviceProvider = unityBuilder.CreateServiceProvider(unityContainer);

        // Retrieve registered services using IServiceProvider
        var myService = serviceProvider.GetRequiredService<IMyService>();
        var myFactory = serviceProvider.GetRequiredService<IMyFactory>();

        // Use the services
        myService.DoSomething();
        myFactory.CreateSomething();
    }
}
```

### Example 2: Managing Objects in Scope

This example demonstrates how to manage objects with the `Scoped` lifecycle, which is useful in web applications (e.g., ASP.NET Core) where objects need to be managed per request.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Voyager.Unity.Builder;
using Unity;

class Program
{
    static void Main(string[] args)
    {
        // Create a ServiceCollection and register a scoped service
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IMyScopedService, MyScopedService>();

        // Create a UnityBuilder
        var unityBuilder = new UnityBuilder();

        // Create a Unity container based on the registered services
        var unityContainer = unityBuilder.CreateBuilder(serviceCollection);

        // Create an IServiceProvider from the Unity container
        var serviceProvider = unityBuilder.CreateServiceProvider(unityContainer);

        // Create a new scope
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedService = scope.ServiceProvider.GetRequiredService<IMyScopedService>();

            // Use the service within the scope
            scopedService.DoScopedWork();
        }

        // The scope is disposed, and the scopedService object is released
    }
}
```

### Explanations:

1. **`CreateBuilder(IServiceCollection services)`**: Creates an `IUnityContainer` based on the registered services in the `ServiceCollection`. This is a key step in integrating Unity with MS DI.
2. **`CreateServiceProvider(IUnityContainer container)`**: Creates an `IServiceProvider` based on the Unity container, allowing the use of registered services in compliance with MS DI conventions.
3. **Scope**: This example shows how to manage the lifecycle of objects within a request context (scope), which is typical in web applications. Once the scope is disposed, the scoped objects are released.

## Support

If you encounter any issues during migration or integration, you can report them directly on the [issues page](https://github.com/Voyager-Poland/Voyager.Unity.Builder/issues) of this repository.

## License

The Voyager.Unity.Builder project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

---

This version of the README now includes the proper installation instructions using NuGet, making it easier for users to install the **Voyager.Unity.Builder** package in their projects. By using `dotnet add package` or the NuGet Package Manager in Visual Studio, users can quickly integrate the library into their development environment.
