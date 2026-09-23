using System;
using System.Collections.Generic;
using System.Text;


namespace ReminderBackgroundService
{
    using NAudio.Wave;
    using NAudio.Wave.Alsa;
    using System.Runtime.InteropServices;

    public interface IAudioService
    {
        void PlayAudio(string filePath);
        void Stop();
    }
    

    public class AudioService : IAudioService, IDisposable
    {
        private IWavePlayer WavePlayer;
        private WaveFileReader? AudioFileReader;

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
        public void PlayAudio(string filePath)
        {

            AudioFileReader = new WaveFileReader(filePath);
            WavePlayer.Init(AudioFileReader);
            
            
            WavePlayer.Play();
        }

        public void Stop()
        {
            WavePlayer?.Stop();
            AudioFileReader?.Dispose();
            WavePlayer?.Dispose();
        }
        public void Dispose()
        {
            WavePlayer?.Dispose();
            AudioFileReader?.Dispose();
        }
    }
}
