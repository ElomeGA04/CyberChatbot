using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace CyberChatbotGUI
{
    public class AudioPlayer
    {
        public void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("CyberWelcome.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error!! The audio file could not be played. {ex.Message}");
            }
        }
    }
}
