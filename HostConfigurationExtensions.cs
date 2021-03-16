using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting.MonoGame
{
    public static class HostConfigurationExtensions
    {
        public static IHostBuilder UseMonoGameLifetime<TGame>(this IHostBuilder hostBuilder)
            where TGame : Game
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<TGame>();
                services.AddSingleton<Game>(x => x.GetRequiredService<TGame>());

                services.AddSingleton<IHostLifetime, MonoGameLifetime<TGame>>();
            });;
        }

        public static Task RunMonoGameAsync<TGame>(this IHostBuilder hostBuilder, CancellationToken cancellationToken = default)
            where TGame : Game
        {
            return hostBuilder.UseMonoGameLifetime<TGame>().Build().RunAsync(cancellationToken);
        }
    }
}
