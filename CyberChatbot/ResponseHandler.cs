using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChatbot
{
    internal class ResponseHandler
    {
        public string Name { get; set; }

        public string GetResponse(string userInput)
        {
            // Basic response logic based on user input
            if (userInput.Contains("How are you") || userInput.Contains("how are u"))
            {
                return $"I am doing well, {Name}! Thank you for asking.";
            }

            //Checking for purpose of the chatbot
            else if (userInput.Contains("purpose"))
            {
                return $"I am a Cyber Chatbot designed to assist you with various tasks and provide information on different topics, {Name}. Topics include online safety, phishing scams, and password sercurity, and safe browsing habits. I can also provide general information and answer questions on anything topic. I will do my best to assit you.";

            }

            //Check for "What can I ask"
            else if (userInput.Contains("what can i ask") || userInput.Contains("what can I ask you"))
            {
                return $"You can ask me anything, {Name}! You can ask about: \nPassword Safety \nPhishing emails and/or calls \nSafe browsing \nSocial Engineering";
            }

            //Check for help
            else if (userInput.Contains("help"))
            {
                return $"Hey, {Name}! I can help you with the following: \nPassword Safety \nPhishing emails and/or calls \n Safe browing practices \n Recognizing social engineering.";
            }

            //Check for password
            else if (userInput.Contains("password"))
            {
                return $"Password Safety Tips:\n\n Use strong password (12+ characters, mix letters, numbers, and symbols) \n Do not reuse passwords across different sites\n Use a password manager to make things easier.\n Enable two-facter authentication where possible";
            }

            //Catchets GetResponse for unrecognized input
            else
            {
                return $"I'm sorry, {Name}, I didn't understand that. Can you please rephrase or ask something else?";
            }

        }
    }
}
