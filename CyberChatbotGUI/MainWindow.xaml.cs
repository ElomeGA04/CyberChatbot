using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberChatbotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// GUI logic for the main window of the CyberChatbot application. 
    /// It initializes the components and sets up the user interface for interaction with the chatbot.
    /// (Button clicks, showing text, etc. are handled in the XAML file and associated event handlers.)
    public partial class MainWindow : Window
    {
        /// FIELDS
        /// The chatbots's brain: Handles all response logic.
        private readonly ChatbotEngine _engine = new ChatbotEngine();

        private readonly AudioPlayer _audio = new AudioPlayer();
        
        /// True once the user has entered their name.
        private bool _nameCaptured = false;
     
        /// Decorative divider string for section header.
        private const string DIV = "==========================================================";

        /// CONSTRUCTOR
        /// Initializes the window, renders the ASCII art, 
        /// plays the voice greeting, and displays the welcome messages.
        public MainWindow()
        {
            InitializeComponent();
            LoadAsciiArt();
            PlayVoiceGreeting();
            ShowInitialWelcome();
        }

        /// STARTUP METHODS
        /// Sets the ASCII art into the header from code-behind.
        /// Uses a raw string literal (@) so line breaks are preserved exactly.
        private void LoadAsciiArt()
        {
            AsciiArtTextBlock.Text =
            @"
 ,-----.         ,--.                                                    ,--.  ,--.                 ,---.                                                                    ,-----.           ,--.   
'  .--./,--. ,--.|  |-.  ,---. ,--.--. ,---.  ,---.  ,---.,--.,--.,--.--.`--',-'  '-.,--. ,--.     /  O  \ ,--.   ,--. ,--,--.,--.--. ,---. ,--,--,  ,---.  ,---.  ,---.     |  |) /_  ,---. ,-'  '-. 
|  |     \  '  / | .-. '| .-. :|  .--'(  .-' | .-. :| .--'|  ||  ||  .--',--.'-.  .-' \  '  /     |  .-.  ||  |.'.|  |' ,-.  ||  .--'| .-. :|      \| .-. :(  .-' (  .-'     |  .-.  \| .-. |'-.  .-' 
'  '--'\  \   '  | `-' |\   --.|  |   .-'  `)\   --.\ `--.'  ''  '|  |   |  |  |  |    \   '      |  | |  ||   .'.   |\ '-'  ||  |   \   --.|  ||  |\   --..-'  `).-'  `)    |  '--' /' '-' '  |  |   
 `-----'.-'  /    `---'  `----'`--'   `----'  `----' `---' `----' `--'   `--'  `--'  .-'  /       `--' `--''--'   '--' `--`--'`--'    `----'`--''--' `----'`----' `----'     `------'  `---'   `--'   
        `---'                                                                        `---'

";
        }

        /// Plays the recorded WAV voice greeting.
        /// Audio failures are caught gracefully so the app never craches.
        private void PlayVoiceGreeting()
        {
            try
            {
                _audio.PlayGreeting();
            }
            catch (Exception ex)
            {
                AddBotMessage("(Voice greeting could not play: " + ex.Message + ")");
            }
        }

        /// Displays the initial welcome banner and prompts for the user's name.
        private void ShowInitialWelcome()
        {
            AddBotMessage("WELCOME TO THE CYBERSECURITY CHATBOT SYSTEM");
            AddBotMessage(DIV);
            AddBotMessage( "Please enter your name: ");
        }

        /// EVENT HANDLERS
        /// Fires when the user presses a key while the input textbox is focused.
        /// Pressing Enter submits the message (same as clicking SEND)
        private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
            }
        }

        /// Fires when the user clicks the SEND button.
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
                ProcessUserInput();
            
        }

        /// Fires when the user clicks the EXIT button.
        /// Closes the entire application.
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        ///CHAT LOGIC
        ///Reads the input textbox, displays it as a user bubble, and returns one of three replies:
        ///1. Name capture
        ///2. Farewell
        ///3. Chatbot response
        private void ProcessUserInput()
        {
            string input = UserInputBox.Text.Trim();

            ///Ignore blank input
            if (string.IsNullOrWhiteSpace(input))
                return;

            AddUserMessage(input);
            UserInputBox.Clear();

            ///First message: capture the user's name
            if (!_nameCaptured)
            {
                _engine.UserName = input;
                _nameCaptured = true;
                AddBotMessage($"Nice to meet you, {_engine.UserName}! You can start asking questions about passwords, phishing, safe browsing, scams, or privacy. Type 'exit' to close.");
                return;
            }
            
            ///Exit command
            if (input.ToLower() == "exit")
            {
                AddBotMessage($"Thank you for chatting with me, {_engine.UserName}. Stay safe online!");
                return;
            }

            ///Get the engine's response and display it
            string response = _engine.GetResponse(input);
            AddBotMessage(response);
            }

        /// CHAT BUBBLE RENDERING
        /// Adds a bot message bubble (left-aligned, red accent).
        private void AddBotMessage(string message)
        {
            AddChatBubble("Bot", message, "#FF6B6B", HorizontalAlignment.Left);
        }

        /// Adds a user message bubble (right-aligned, blue accent).
        private void AddUserMessage(string message)
        {
            AddChatBubble("YOU", message, "#4FC3F7", HorizontalAlignment.Right);
        }

        /// Creates and append a styled chat bubble to te chat panel
        /// The bubble is aligned left or right depending on who sent it and has a colored bottom border tha matched the sender's accent.
        private void AddChatBubble(string sender, string message, string accentColor, HorizontalAlignment alignment)
        {
            ///Outer container
            var container = new Border
            {
                HorizontalAlignment = alignment,
                Margin = new Thickness(5, 4, 5, 4),
                MaxWidth = 650
            };

            ///Inner bubble
            var bubble = new Border
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#333333"),
                BorderBrush = (Brush)new BrushConverter().ConvertFromString(accentColor),
                BorderThickness = new Thickness(0, 0, 0, 3),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12, 8, 12, 8)
            };

            ///Sender label
            var stack = new StackPanel();

            var senderLabel = new TextBlock
            {
                Text = sender,
                Foreground = (Brush)new BrushConverter().ConvertFromString(accentColor),
                FontWeight = FontWeights.Bold,
                FontSize = 11,
                Margin = new Thickness(0, 0, 0, 3)
            };

            ///Message text: Wraps if text is long
            var messageText = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap
            };

            stack.Children.Add(senderLabel);
            stack.Children.Add(messageText);
            bubble.Child = stack;
            container.Child = bubble;

            ///Add to the chat panel and scroll to reveal it
            ChatStackPanel.Children.Add(container);
            ChatScrollViewer.ScrollToEnd();
        }
    }
}

