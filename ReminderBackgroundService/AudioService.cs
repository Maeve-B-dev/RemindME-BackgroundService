using System;
using System.Collections.Generic;
using System.Text;


namespace ReminderBackgroundService
{
    using NAudio.Wave;
    using NAudio.Wave.Alsa;
    using System.Runtime.InteropServices;

    //Main Interface. Access these functions in Workers to play audio.
    public interface IAudioService
    {
        void PlayAudio(string filePath);
        void Stop();
    }
    
    // Main AudioService class.
    public class AudioService : IAudioService, IDisposable
    {
        // Initialize variables as private
        private IWavePlayer WavePlayer;
        private WaveFileReader? AudioFileReader;

        // Set player for given OS or report an Error if the OS is not recognized.
        public AudioService()
        {
            if(OperatingSystem.IsWindows())
            {
                WavePlayer = new WasapiPlayerBuilder().Build();
            }else if(OperatingSystem.IsLinux())
            {
                WavePlayer = new AlsaOut("pulse");
            }else
            {
                throw new PlatformNotSupportedException("Not supported on this Plattform!");
            }
        }
        // Main Audio Playing function.
        public void PlayAudio(string filePath)
        {
            AudioFileReader = new WaveFileReader(filePath);
            WavePlayer.Init(AudioFileReader);
            
            
            WavePlayer.Play();
        }
        //Stopping function. Will also dispose of WavePlayer and FileReader!
        public void Stop()
        {
            WavePlayer?.Stop();
            Dispose();
        }
        public void Dispose()
        {
            WavePlayer?.Dispose();
            AudioFileReader?.Dispose();
        }
    }
}
