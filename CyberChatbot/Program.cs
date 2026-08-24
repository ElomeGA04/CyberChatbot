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
            Console.ReadKey();
        }
        
    }
}