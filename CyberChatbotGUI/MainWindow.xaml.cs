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
        ///Repository handles all communication with MySql database
        ///We instantiate it once here so it is ready whenever we need to save oload tasks
        private readonly TaskRepository _taskRepo = new TaskRepository();

        ///ObservableCollection automatically updates the UI (DataGrid) whenever items are added, removed, or modified
        private System.Collections.ObjectModel.ObservableCollection<SecurityTask> _tasksList = new System.Collections.ObjectModel.ObservableCollection<SecurityTask>();
        /// FIELDS
        /// The chatbots's brain: Handles all response logic.
        private readonly ChatbotEngine _engine = new ChatbotEngine();

        private readonly AudioPlayer _audio = new AudioPlayer();

        /// True once the user has entered their name.
        private bool _nameCaptured = false;

        /// 
        private QuizEngine _quizEngine;

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
            LoadTasks(); //Load any tasks that were previously saved in the database so they appaer as soon as the user opens the Task tab.

            

        }

        /// STARTUP METHODS
        /// Sets the ASCII art into the header from code-behind.
        /// Uses a verbatim string (@) so line breaks are preserved exactly.
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
            AddBotMessage("Please enter your name: ");
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
        private async Task ProcessUserInput()
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

            //Checks if the bot detected an NLP task command 
            if (response.StartsWith("NLP_TASK:"))
            {
                string extractedTask = response.Replace("NLP_TASK:", "");

                //Creates the task automatically
                var newTask = new SecurityTask { Title = extractedTask, IsCompleted = false };

                //Save to database using your existing repo
                await _taskRepo.AddTaskAsync(newTask);
                _engine.BotActivityLog.AddEntry($"Task added via UI: '{newTask.Title}'");
                LoadTasks(); //Refreshes the UI table

                AddBotMessage($"Got it! I've added '{extractedTask}' to your Task Assistant.");
            }
            else
            {
                //Normal chat response
                AddBotMessage(response);
            }
            
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

        /// Creates and append a styled chat bubble to the chat panel
        /// The bubble is aligned left or right depending on who sent it and has a colored bottom border that matched the sender's accent.
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

        ///TASK ASSISTANT LOGIC (DATABASE)
        ///Fetches all tasks from MySql database and displays them in te DataGrid
        ///Wrapped in a try-catch block to prevent the app from crashing if the database is offline.
        private async void LoadTasks()
        {
            try
            {
                //Ask the repository to fetch all tasks from the database
                var tasksFromDb = await _taskRepo.GetAllTasksAsync();

                //Clear the current local list and replace it with the fresh database data
                _tasksList.Clear();
                foreach (var task in tasksFromDb)
                {
                    _tasksList.Add(task);
                }

                //Bind the list to the DataGrid so the user can see the tasks
                TasksDataGrid.ItemsSource = _tasksList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load tasks from database: " + ex.Message);
            }

        }


        /// Triggered when the user clicks "Add task".
        /// Validates input, creates a new task, saves it to MySql, and refreshes the UI.

        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Validation: Ensure the user actually typed a title
            if (string.IsNullOrWhiteSpace(TaskTitleBox.Text))
            {
                MessageBox.Show("Please enter a task title before adding a task.");
                return;
            }

            // 2. Create the new task object using the data from the textboxes
            var newTask = new SecurityTask
            {
                Title = TaskTitleBox.Text,
                Description = TaskDescBox.Text,
                IsCompleted = false
            };

            // 3. Parse the reminder. If the user typed a number (e.g., "3"), set the reminder date to 3 days from today.
            if (int.TryParse(TaskReminderBox.Text, out int days))
            {
                newTask.ReminderDate = DateTime.Now.AddDays(days);

            }

            try
            {
                // 4. Send the task to the TaskRepository to be saved in MySql
                await _taskRepo.AddTaskAsync(newTask);

                // 5. Refresh the DataGrid so the new task appears immediately
                LoadTasks();

                // 6. Clear the textboxes so the user can easily add another task
                TaskTitleBox.Clear();
                TaskDescBox.Clear();
                TaskReminderBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving task: " + ex.Message);
            }
        }

        ///Triggered when the user clicks the green checkmark button.
        ///Marks the selected task as completed in the database.

        private async void MarkCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            // btn.DataContext holds the specific SecurityTask for that row
            if (sender is Button btn && btn.DataContext is SecurityTask task)
            {
                await _taskRepo.MarkCompleteAsync(task.Id); // Send the task's ID to the Database to update its 'is_completed'

                LoadTasks(); //Refresh the grid to show the task is now complete
            }
        }

        ///Triggered when the user clicks the red X button.
        /// Permanently deletes the selected task from the database

        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            //We find the specific task attached to the button that was clicked
            if (sender is Button btn && btn.DataContext is SecurityTask task)
            {
                await _taskRepo.DeleteTaskAsync(task.Id); // Send the task's ID to the database to be deleted

                LoadTasks(); //Refresh the grid to remove the deleted task from the UI
            }
        }


        /// PART 3: QUIZ LOGIC
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            _engine.BotActivityLog.AddEntry("User started the Cybersecurity Quiz");
            //Initialize a fresh quiz engine
            _quizEngine = new QuizEngine();

            QuizStartPanel.Visibility = Visibility.Collapsed;
            QuizEndPanel.Visibility = Visibility.Collapsed;
            QuizQuestionPanel.Visibility = Visibility.Visible;

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            var q = _quizEngine.GetCurrentQuestion();

            QuestionProgressText.Text = $"Question {_quizEngine.CurrentQuestionIndex + 1} of {_quizEngine.Questions.Count}";
            QuestionTextBlock.Text = q.QuestionText;

            OptionAButton.Content = $"A) {q.OptionA}";
            OptionBButton.Content = $"B) {q.OptionB}";

            //Handle True/False questions
            if (string.IsNullOrEmpty(q.OptionC))
            {
                OptionCButton.Visibility = Visibility.Collapsed;
                OptionDButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                OptionCButton.Visibility = Visibility.Visible;
                OptionDButton.Visibility = Visibility.Visible;
                OptionCButton.Content = $"C) {q.OptionC}";
                OptionDButton.Content = $"D) {q.OptionD}";
            }

            //Reset UI states
            FeedbackTextBlock.Text = "";
            NextQuestionButton.Visibility = Visibility.Collapsed;

            OptionAButton.IsEnabled = true;
            OptionBButton.IsEnabled = true;
            OptionCButton.IsEnabled = true;
            OptionDButton.IsEnabled = true;

            OptionAButton.Background = (Brush)new BrushConverter().ConvertFromString("#2D2D2D");
            OptionBButton.Background = (Brush)new BrushConverter().ConvertFromString("#2D2D2D");
            OptionCButton.Background = (Brush)new BrushConverter().ConvertFromString("#2D2D2D");
            OptionDButton.Background = (Brush)new BrushConverter().ConvertFromString("#2D2D2D");

        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                string selectedOption = btn.Tag.ToString();
                var q = _quizEngine.GetCurrentQuestion();

                bool isCorrect = _quizEngine.SubmitAnswer(selectedOption);
                
                // 1. Disable all buttons they can't double-click
                OptionAButton.IsEnabled = false;
                OptionBButton.IsEnabled = false;
                OptionCButton.IsEnabled = false;
                OptionDButton.IsEnabled = false;

                // 2. Adds the colors!
                if (isCorrect)
                {
                    //If they got it right: Turns the clicked button green
                    FeedbackTextBlock.Foreground = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
                    FeedbackTextBlock.Text = "✔️ Correct!" + q.Explanation;
                    btn.Background = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
                }

                else
                {
                    //If they got it wrng: Turns the clicked button red
                    FeedbackTextBlock.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF6B6B");
                    FeedbackTextBlock.Text = $"❌ Incorrect. Correct answer: {q.CorrectAnswer}. {q.Explanation}";
                    btn.Background = (Brush)new BrushConverter().ConvertFromString("#FF6B6B");

                    //Highlights the actual correct answer in Green
                    if (q.CorrectAnswer == "A") OptionAButton.Background = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
                    if (q.CorrectAnswer == "B") OptionBButton.Background = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
                    if (q.CorrectAnswer == "C") OptionCButton.Background = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
                    if (q.CorrectAnswer == "D") OptionDButton.Background = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
                }

                NextQuestionButton.Visibility = Visibility.Visible;
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (_quizEngine.MoveToNextQuestion())
            {
                LoadQuestion();
            }

            else
            {
                QuizQuestionPanel.Visibility = Visibility.Collapsed;
                QuizEndPanel.Visibility = Visibility.Visible;

                //Calculates the score nd the percentage
                int score = _quizEngine.Score;
                int total = _quizEngine.Questions.Count;
                double percentage = Math.Round((double)score / total * 100);

                //Display the score with the percentage included
                FinalScoreText.Text = $"You scored {score}/{total} ({percentage}%)";
                FinalMessageText.Text = _quizEngine.GetFinalMessage();
            }
        }
            private void ShowLogButton_Click(object sender, RoutedEventArgs e)
            {
                AddUserMessage("[Opened Activity Log]");
                AddBotMessage(_engine.BotActivityLog.GetRecentLog());
            }
        }
    }

