using System;

namespace CyberChatbot

{
    //Handles the chatbot's response logic based on the user's input.
    //Has personalized responses using the user's name.
    internal class ResponseHandler
    {
        //Gets or sets the user's name for personalized responses. Default is "friend" if no name is provided.
        public string Name { get; set; } = "friend";

        // Processes the user's input and generates an appropriate response based on predefined keywords or phrases.
        // Falls back to a default response if the input doesn't match any known patterns.
        public void GetResponse(string userInput)
        {
            //Normalize the input for easier comparison
            //Trim whitespace and convert to lowercase for case-insensitive comparison
            string question = userInput.ToLower().Trim();

            // Check for specific keywords or phrases in the user's input and respond accordingly
            // Note: "exit" is handled in Program.cs loop, so it is not included here.

            // General responses
            if (question.Contains("how are you"))
            {
                Console.WriteLine($"I am doing well {Name} and ready to help you.");
            }
            else if (question.Contains("purpose"))
            {
                Console.WriteLine($"The purpose is to assist you with cyber safety information and guidance, {Name}.");
            }

            // Topic responses
            else if (question.Contains("what can i ask") || question.Contains("topic"))
            {
                Console.WriteLine($"{Name}, you can ask about passwords, phishing and safe browsing.");
            }

            // Cyber safety responses
            else if (question.Contains("password"))
            {
                Console.WriteLine($"{Name}, use strong, unique passwords for each account and consider using a password manager.");
            }
            else if (question.Contains("phishing"))
            {
                Console.WriteLine($"{Name}, be cautious of suspicious emails or messages asking for personal information. Verify the source before clicking any links.");
            }
            else if (question.Contains("safe browsing"))
            {
                Console.WriteLine($"{Name}, use secure websites (https), avoid clicking on unknown links, and keep your browser updated.");
            }

            // Default response for unrecognized input
            else
            {
                Console.WriteLine($"Sorry, {Name}, I didn't quite understand that. Could you rephrase it?");
            }
        }
    }
}
