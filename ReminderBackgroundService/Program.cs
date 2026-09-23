using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace ReminderBackgroundService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            if (OperatingSystem.IsWindows())
            {
                builder.Services.AddWindowsService();
            }
            //Add builder Audio Service(AudioService.cs) and worker(Worker.cs)
            builder.Services.AddSingleton<IAudioService, AudioService>();
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}
