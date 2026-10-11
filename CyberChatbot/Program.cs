using System;
namespace CyberChatbot
{

    // The main entry point for the CyberChatbot application. It initializes the chatbot, handles user input, and manages the conversation flow.
    internal class Program
    {
        // Application entry point. Initializes the chatbot, prompts for user input, and manages the conversation loop until the user decides to exit.
        static void Main()
        {
            // Voice greeting: Create an instance of the AudioPlayer class to play the welcome message.
            AudioPlayer audio = new AudioPlayer();
            audio.PlayWelcomeMessage();

            // ASCII art: Display a visually appealing ASCII art banner to welcome the user to the Cyber Chatbot application.
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"====================================================================================================================================================================================================");
            Console.WriteLine(@"                                                                                                                                                                                                      
 ,-----.         ,--.                                                    ,--.  ,--.                 ,---.                                                                    ,-----.           ,--.   
'  .--./,--. ,--.|  |-.  ,---. ,--.--. ,---.  ,---.  ,---.,--.,--.,--.--.`--',-'  '-.,--. ,--.     /  O  \ ,--.   ,--. ,--,--.,--.--. ,---. ,--,--,  ,---.  ,---.  ,---.     |  |) /_  ,---. ,-'  '-. 
|  |     \  '  / | .-. '| .-. :|  .--'(  .-' | .-. :| .--'|  ||  ||  .--',--.'-.  .-' \  '  /     |  .-.  ||  |.'.|  |' ,-.  ||  .--'| .-. :|      \| .-. :(  .-' (  .-'     |  .-.  \| .-. |'-.  .-' 
'  '--'\  \   '  | `-' |\   --.|  |   .-'  `)\   --.\ `--.'  ''  '|  |   |  |  |  |    \   '      |  | |  ||   .'.   |\ '-'  ||  |   \   --.|  ||  |\   --..-'  `).-'  `)    |  '--' /' '-' '  |  |   
 `-----'.-'  /    `---'  `----'`--'   `----'  `----' `---' `----' `--'   `--'  `--'  .-'  /       `--' `--''--'   '--' `--`--'`--'    `----'`--''--' `----'`----' `----'     `------'  `---'   `--'   
        `---'                                                                        `---'                                                                                                             ");

            Console.WriteLine(@"====================================================================================================================================================================================================");
            Console.ResetColor();
            Console.WriteLine();

            // Welcome message: Display a welcome message to the user, indicating that the Cyber Chatbot is ready to assist with cyber safety information and guidance.
            Console.WriteLine("==========================================================");
            Console.WriteLine(" WELCOME TO THE CYBER CHATBOT! WE ARE HAPPY TO ASSIST YOU!");
            Console.WriteLine("==========================================================");

            // Get and validate the user's name
            ResponseHandler handle = new ResponseHandler();

            Console.Write($"Please enter your name: ");
            string? nameInput = Console.ReadLine();


            //Name validation process
            while (string.IsNullOrWhiteSpace(nameInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty!! ");
                Console.ResetColor();
                Console.Write("Please enter your name: ");
                nameInput = Console.ReadLine();
            }

            //Only assign after validation, handle.Name is now guaranteed to be valid.
            handle.Name = nameInput;

            //Personalized user greeting
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nWelcome, {handle.Name}! You can start chatting with the Cyber Chatbot now.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("==================================================================");
            Console.ResetColor();

            // Boolean flag keeps the loop running until the user types "exit"
            bool keepChatting = true;
            while (keepChatting)
            {
                // Prompt the user for a question
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ask a question or type 'exit': What can I ask you about");
                Console.ResetColor();

                // Null-safe input handling: Read user input and check for null or whitespace. If the input is invalid, prompt the user to enter a valid question.
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a valid question.");
                    continue;
                }

                // Normalize the input for easier comparison: Trim whitespace and convert to lowercase for case-insensitive comparison.
                string question = input.ToLower().Trim();

                // Exit condition. Farewell and end the loop
                if (question == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Goodbye! Stay safe online.");
                    Console.ResetColor();

                    keepChatting = false;

                    continue;
                }

                // Get and display the chatbot's response using the ResponseHandler class. The response is generated based on predefined keywords or phrases in the user's input.
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\nChatbot: ");

                //Typing effect
                var writer = new System.IO.StringWriter();
                var orig = Console.Out;
                Console.SetOut(writer);
                handle.GetResponse(question);
                Console.SetOut(orig);
                string text = writer.ToString();

                foreach (char c in text)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(20);
                }

                Console.WriteLine();
                Console.ResetColor();

                // Divider between exchanges for readability
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("==================================================================");
                Console.ResetColor();

                Console.WriteLine();
            }

            // Farewell message
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Thank you for chatting with me, {handle.Name}!");
            Console.WriteLine("Remember: Stay safe, Stay secure, Stay smart");
            Console.WriteLine();
            Console.WriteLine("Goodbye!");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("==================================================================");
            Console.ResetColor();

            // Waits for the user to press any key before closing the application.
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

    }
}
