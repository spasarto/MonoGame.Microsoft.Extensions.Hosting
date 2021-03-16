using Microsoft.Xna.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting.MonoGame
{
    public class MonoGameLifetime<TGame> : IHostLifetime
        where TGame : Game
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly TGame _game;

        public MonoGameLifetime(TGame game, IHostApplicationLifetime applicationLifetime)
        {
            _game = game;
            _applicationLifetime = applicationLifetime;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _game.Dispose();
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(() =>
            {
                _game.Exiting += (o, e) =>
                {
                    _applicationLifetime.StopApplication();
                };
                _game.Run();
            });

            return Task.CompletedTask;
        }
    }
}
