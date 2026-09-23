
namespace ReminderBackgroundService
{
    public class Worker(ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
    {
        // Set default variables. Will be overwritten by any config setup in appsettings.json
        int delay = 900000;
        string audioPath = "./alert.wav";
        // Setup the audioService by creating the Object.
        private IAudioService audioService = new AudioService();

        // Called when the worker starts. Gets variables from appsettings.json, logs errors if they are unreadable.
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ReminderWorker is starting! Detecting and setting AppSettings from appsettings.json...");
            // Check TimeInMS config and overwrite delay.
            if(!int.TryParse(configuration["AppSettings:TimeInMS"], out delay))
            {
                logger.LogError("Could not read TimeInMS configuration, using default Value. Value read was: " + configuration["AppSettings:TimeInMS"]);
            }else
            {
                logger.LogInformation("Read TimeInMS as: " + delay);
            }
            // Check filePath config and overwrite audioPath.
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
        // Main Worker function, loops until Worker is stopped.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                // Recreate AudioService. This is necessary for reinitialization of the NAudio objects.
                audioService = new AudioService();
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                // Try playing the Audio from audioPath. Errors will be logged, but will not necisarrily stop service execution.
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
