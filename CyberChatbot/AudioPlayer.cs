using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace CyberChatbot
{
    public class AudioPlayer
    {
        public void PlayWelcomeMessage()
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
