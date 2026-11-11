using System.IO;
using System.Media;

namespace CyberChatbotGUI
{
    /// <summary>
    /// Plays the recorded WAV voice greeting.
    /// </summary>
    internal class AudioPlayer
    {
        public void PlayGreeting(string fileName = "CyberWelcome.wav")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            SoundPlayer player = new SoundPlayer(path);
            player.Play();
        }
    }
}
