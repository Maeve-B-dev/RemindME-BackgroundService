using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ReminderBackgroundService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            
            builder.Services.AddSingleton<IAudioService, AudioService>();
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}
