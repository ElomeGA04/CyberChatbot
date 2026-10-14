using System.Text;
using System.Windows;
using System.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private readonly ChatbotEngine _engine = new ChatbotEngine();
        private bool _nameCaptured = false;
        private const string DIV = "==========================================================";

        public MainWindow()
        {
            InitializeComponent();

            LoadAsciiArt();

            PlayVoiceGreeting();

            ShowInitialWelcome();
        }

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

        private void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("CyberWelcome.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                AddBotMessage("(Voice greeting could not play: " + ex.Message + ")");
            }
        }

        private void ShowInitialWelcome()
        {
            AddBotMessage("Chat", DIV);
            AddBotMessage("Chat", "WELCOME TO THE CYBERSECURITY CHATBOT SYSTEM");
            AddBotMessage("Chat", DIV);
            AddBotMessage("Chat", "Please enter your name: ");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        ///CHAT LOGIC
        
        private void ProcessUserInput()
        {
            string input = UserInputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return;

            AddUserMessage(input);
            UserInputBox.Clear();

            if (!_nameCaptured)
            {
                _engine.UserName = input;
                _nameCaptured = true;
                AddBotMessage($"Nice to meet you, {_engine.UserName}! You can start asking questions about passwords, phishing, or safe browsing. Type 'exit' to close.");
                return;
            }
            
            if (input.ToLower() == "exit")
            {
                AddBotMessage($"Thank you for chatting with me, {_engine.UserName}. Stay safe online!");
                return;
            }

            string response = _engine.GetResponse(input);
            AddBotMessage(response);
            }

        private void AddBotMessage(string message)
        {
            AddChatBubble("Bot", message, "#FF6B6B", HorizontalAlignment.Left);
        }

        private void AddUserMessage(string message)
        {
            AddChatBubble("YOU", message, "4FC3F7", HorizontalAlignment.Right);
        }

        private void AddChatBubble(string sender, string message, string accentColor, HorizontalAlignment)
        {
            var container = new Border
            {
                HorizontalAlignment = alignment,
                Margin = new Thickness(5, 4, 5, 4),
                MaxWidth = 650
            };

            var bubble = new Border
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#333333"),
                BorderBrush = (Brush)new BrushConverter().ConvertFromString(accentColor),
                BorderThickness = new Thickness(0, 0, 0, 3),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12, 8, 12, 8)
            };

            var stack = new StackPanel();

            var senderLabel = new TextBlock
            {
                Text = sender,
                Foreground = (Brush)new BrushConverter().ConvertFromString(accentColor),
                FontWeight = FontWeights.Bold,
                FontSize = 11,
                Margin = new Thickness(0, 0, 0, 3)
            };

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

            ChatStackPanel.Children.Add(container);
            ChatScrollViewer.ScrollToEnd();
        }
    }
}

