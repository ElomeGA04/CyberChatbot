using System;
namespace CyberChatbot
{
    internal class  Program
    {
        static void Main()
        {
            AudioPlayer audio = new AudioPlayer();
            audio.PlayWelcomeMessage();

            Console.ForegroundColor = ConsoleColor.Cyan;
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

            Console.WriteLine("==========================================================");
            Console.WriteLine(" WELCOME TO THE CYBER CHATBOT! WE ARE HAPPY TO ASSIST YOU!");
            Console.WriteLine("==========================================================");

            ResponseHandler handle = new ResponseHandler();
            
            Console.WriteLine($"Please enter your name: {handle.Name}");
            handle.Name = Console.ReadLine();
            Console.WriteLine($"\nWelcome, {handle.Name}! You can start chatting with the Cyber Chatbot now.");

            Console.WriteLine("Press any key to exit...");

            //Name validation process
            while(string.IsNullOrWhiteSpace(handle.Name))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Name cannot be empty!! ");
                Console.ResetColor();
                Console.Write("Please enter your name: ");
                handle.Name = Console.ReadLine();
            }

            //Personalized user greeting
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nWelcome, {handle.Name}! You can start chatting with the Cyber Chatbot now.");
            Console.ResetColor();
            Console.WriteLine("==================================================================");
            Console.ReadLine();
            Console.ReadKey();
        }
        
    }
}