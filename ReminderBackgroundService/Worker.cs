
namespace ReminderBackgroundService
{
    public class Worker(ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
    {
        int delay = 900000;

        string audioPath = "./alert.wav";

        private IAudioService audioService = new AudioService();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ReminderWorker is starting! Detecting and setting AppSettings from appsettings.json...");
            if(!int.TryParse(configuration["AppSettings:TimeInMS"], out delay))
            {
                logger.LogError("Could not read TimeInMS configuration, using default Value. Value read was: " + configuration["AppSettings:TimeInMS"]);
            }else
            {
                logger.LogInformation("Read TimeInMS as: " + delay);
            }
            if(configuration["AppSettings:filePath"] != null)
            {
                audioPath = configuration["AppSettings:filePath"];
                logger.LogInformation("Read filePath as: " + audioPath);
            }else
            {
                logger.LogError("Could not read filePath configuration, using default Value.");
            }
            
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                audioService = new AudioService();
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                try
                {
                    audioService.PlayAudio(audioPath);
                    await Task.Delay(3000, stoppingToken);
                    audioService.Stop();
                }catch (Exception e)
                {
                    logger.LogError(e, "Error in Worker!");
                }
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
