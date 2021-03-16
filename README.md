# MonoGame.Microsoft.Extensions.Hosting

Adds a MonoGame lifetime to the Microsoft Extensions Hosting platform.

Usage:

```csharp
    public static class Program
    {
        [STAThread]
        static Task Main()
        {
            return new HostBuilder()
                .RunMonoGameAsync<MyGame>();
        }
    }
```