using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChatbot
{
    internal class ResponseHandler
    {
        public string Name { get; set; }

        public void GetResponse(string userInput)
        {
            string question;
            do
            {
                Console.WriteLine("Ask a question or type 'exit': What can I ask you about");
                question = Console.ReadLine().ToLower().Trim();

                Console.WriteLine("\nAssistant: ");

                if (question == "exit")
                {
                    Console.WriteLine("Goodbye! Stay safe online.");
                }
                else if (question.Contains("how are you"))
                {
                    Console.WriteLine("I am doing well and ready to help you.");
                }
                else if (question.Contains("purpose"))
                {
                    Console.WriteLine("The purpose is to teach you about cyber safety.");
                }
                else if (question.Contains("what can i ask") || question.Contains("topic"))
                {
                    Console.WriteLine("You can ask me about passwords, phishing, and safe browsing.");
                }
                else if (question.Contains("password"))
                {
                    Console.WriteLine("Create a strong password using letters, numbers, and special letters.");
                }
                else if (question.Contains("phishing"))
                {
                    Console.WriteLine("Don't open suspisious links.");
                }
                else if (question.Contains("safe browsing"))
                {
                    Console.WriteLine("Use trusted websites.");
                }
                else
                {
                    Console.WriteLine("I didn't quite understand that, could you rephrase.");
                }
            }
            while (question != "exit");

        }
    }
}
