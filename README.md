
# CYBERCHATBOT: CYBERSECURITY AWARENESS BOT
 
 # Description
 The Cybersecurity Awareness Bot is a console-based chatbot written in C#(.NET) that educates users about basic cybersecurity practices in an interactive, conversational manner. 
 The bot greets the user with a recorded voice message, displays a cybersecurity-themed ASCII art image, and asks the user's name to personalize the conversation, 
 and answers questions about topics such as password safety, phishing, and safe browsing.

 The application demonstrates core programming concepts, including input validation, 
 string handling, control flow, methods, classes, console formatting (colors, borders, dividers), 
 audio playback with System.Media, and a typing effect to simulate natural conversation.

 # Features
Voice greeting:
Plays a recorded WAV audio message (CyberWelcome.wav) welcoming the user when the application launches.

ASCII Art Image:
Displays a "Cybersecurity Awareness Bot" ASCII art title screen (The console window must be zoomed in/ minimized to see the full ASCII art properly.)

Personalized greeting:
Asks the user for their name and uses it throughout the conversation.

Basic response system:
Answers questions such as "How are you?", "What is your purpose?", and "What can I ask you about?"

Cybersecurity topics:
Responds to password safety, phishing, and safe browsing.

Input validation:
Detects empty or unsupported input and responds gracefully: "I didn't quite understand that. Could you rephrase?"

Enhanced console UI:
Uses colored text, decorative borders, section headers, and dividers for a structured interface.

Typing effect:
Simulates a conversational feel with slight delays when the bot types its responses.

Graceful exit:
Typing 'exit' ends the chat with a personalized goodbye message.

# Requirements
- .NET SDK 10.0 or higher
- Visual Studio 2022/2026 or any C# IDE
- Windows OS (for audio playback support)
- Git (for version control)

# Installation and Setup
Step 1: Clone the repository
bash
git clone https://github.com/ElomeG40/CyberChatbot.git
cd CyberChatbot

Step 2: Open in Visual Studio
1. Navigate to the project folder
2. Double-click CyberChatbot.csproj to open in Visual Studio.
3. Or open Visual Studio > Open Project > Select CyberChatbot.csproj

Step 3: Restore dependencies
dotnet restore

Step 4: Run the application
dotnet run

# How to run
dotnet run

Usage:
1. When the application launches, the voice greeting plays and the ASCII art image appears.
2. Enter your name when prompted.
3. Ask the bot questions, for example:
	~ how are you
	~ what is your purpose
	~ what can I ask you
	~ passwords
	~ phishing
	~ safe browsing
4. Type 'exit' at any time to end the conversation.


# Project structure

CyberChatbot/
	---Program.cs						**Main application entry point**						
	---ResponseHandler.cs				**Chatbot responses, topic logic and input**		
	---AudioPlayer.cs					**Plays the recorded voice greeting**
	---CyberWelcome.wav					**Recorded voice greeting (played when the application starts)**
	---README.md						**Project documentation**
	--- .github/
		---workflows/
			---ci.yml					**Github actions CI workflow**
	---screenshot/
		---ci-pass.png					**Screenshot of a successful CI run**

# Class descriptions
Program.cs
Main application entry point. Handles the UI, color formatting, typing effect, and user-interaction loop.

AudioPayer.cs
Plays the welcome voice message using System.Media.SoundPlayer.

ResponseHandler.cs
Contains logic for processing user input and generating appropriate responses based on predefined topics (password safety, phishing, safe browsing).

CyberWelcome.wav
Audio file played when the application starts.

# Continuous Integration (GitHub Actions)
This repository uses GitHub Actions for Continuous Integration (CI). On every push, the CI workflow automatically:
1. Restores dependencies
2. Builds the project (dotnet build)
3. Checks for syntax errors and formatting/build issues
4. Runs on every push and pull request to the main branch

# Successful CI Run
screenshot/ci-pass.png shows a successful CI run with all steps completed without errors.

# Cybersecurity topics covered
Password safety:
Use strong, unique passwords for each account and consider using a password manager to store them securely.

Phishing:
Be cautious of suspicious emails or messages asking for personal information. Verify the source before clicking any links

Safe browsing:
Use secure websites (HTTPS), avoid clicking on unknown links, and keep your browser updated.

# Video Presentation
Unlisted YouTube link: https://youtu.be/bAEvqBl0ziE

The presentation demonstrates:
1. Code structure and class relationships between Program.cs, ResponseHandler.cs, and AudioPlayer.cs
2. Voice greeting integration using System.Media.SoundPlayer
3. Console UI formatting techniques including colored text, borders, and typing effect
4. Input validation logic for empty entries and unsupported queries
5. CI pipeline setup and successful run using GitHub Actions
6. Application functionality showcasing the chatbot's responses to various cybersecurity topics

# CYBERCHATBOT: CYBERSECURITY AWARENESS BOT- PART 2
A WPF GUI chatbot, built in C# , that educates users on cybersecurity awareness. The application detects user sentiment, remembers details shared during conversation, provides random responses per topic, and maintains natural conversational flow.

# Overview
The Cybersecurity Awareness Bot is a console and GUI application that helps users learn how to stay safe online. Part 2 extends the Part 1 console chatbot with a graphical user interface built using Windows Presentation Foundation (WPF), and adds advanced features including sentiment detection, memory recall, random response selection, and conversational flow.
The chatbot covers five cybersecurity topics:
1. Password safety
2. Phishing
3. Safe browsing
4. Scams
5. Privacy

# Features
1. Graphical User Interface (GUI) - Dark-themed WPF window with colored chat bubbles for user and bot messages
2. Sentiment detection - Detects worried, curious, and frustrated tones and adds an empathetic prefix to the reply.
   - Worried: "worried", "scared", "nervous", "anxious", "afraid"
   - Curious: "curious", "wondering"
   - Frustrated: "frustrated", "annoyed", "confused", "angry", "stressed"
3. Memory and recall - Remembers the user's name and favourite topic, then refers back to them during later conversation
4. Random responses - Four or five detailed responses per topic, excluding Sentiments (which contains one response per sentiment), selected at random for variety
5. Conversational flow - Commands such as "tell me more" and "explain more" continue the previous topic without restarting
6. Keyword recognition - Matches user input against a dictionary of cybersecurity topics
7. Error handling - Provides a helpful fallback message for unrecognized input without crashing
8. Voice greeting - Plays the recorded welcome message when the application starts

# How to use

1. When the application starts, the voice greeting plays and the ASCII art logo appears in the header.
2. Enter your name when prompted.
3. Ask about any of the following topics:
   - password
   - phishing
   - safe browsing
   - scam
   - privacy
4. Use the following commands to interact further:
   - **tell me more** (also "explain more", "another tip", "more info"): continues the last topic with a new response
   - **I'm interested in [topic]**: saves the topic to memory
   - **what am I interested in** (also "what do you remember"): recalls the saved topic
   - **what is my name**: recalls the saved name
   - **help** (also "tip", "advice"): gives a tip about your saved favourite topic
   - **I'm worried / curious / frustrated about [topic]**: triggers an empathetic response before the tip
   - **how are you**, **what is your purpose**, **what can I ask you**: general conversation
   - **exit**: ends the conversation with a goodbye message. The **EXIT** button closes the application.

If the voice greeting does not play, a message appears in the chat. Check that CyberWelcome.wav is set to copy to the output directory in CyberChatbotGUI.csproj.

# Class descriptions

- **App.xaml / App.xaml.cs:** Application entry point. Starts the main window.
- **MainWindow.xaml:** GUI layout with three rows: the header with the ASCII art, the chat history in a ScrollViewer, and the input row. The input row uses a DockPanel so the text box, SEND button, and EXIT button sit side by side.
- **MainWindow.xaml.cs:** Loads the ASCII art, plays the voice greeting, captures the users name on the first message, handles the exit command, sends all other input to ChatbotEngine.GetResponse, and draws the chat bubbles.
- **ChatbotEngine.cs:** The core logic.
  - Memory: UserName, FavouriteTopic, and _lastTopic.
  - Data: a Dictionary<string, List<string>> of topic responses.
  - Methods: GetResponse, DetectSentiment, GetSentimentPrefix, and GetRandomResponse.
- **AudioPlayer.cs:** PlayGreeting builds the path to CyberWelcome.wav from the applications folder and plays it with SoundPlayer. MainWindow calls it inside a try/catch.

  
# Project Structure
CyberChatbot/
	---CyberChatbot/						**Part 1 - Console application
		---Program.cs
		---ResponseHandler.cs
		---AudioPlayer.cs
		---CyberChatbot.csproj
		---CyberWelcome.wav
	---CyberChatbotGUI/						**Part 2 - WPF GUI application
		---App.xaml
			---App.xaml.cs					Application entry point
		---MainWindow.xaml					GUI layout
			---MainWindow.xaml.cs			GUI event handlers and chat bubble rendering
		---ChatbotEngine.cs					Core logic: keywords, sentiment, memory, random responses
		---AudioPlayer.cs					Voice greeting playback
		---CyberChatbotGUI.csproj
		---CyberWelcome.wav
	---.github/
		---workflows/
			---ci.yml						Github Actions CI pipeline
	---global.json							Pins the .NET SDK version
	---README.md
	---gitignore

# Requirements
- .NET SDK 10.0 or higher
- Windows operating system (required for System.Media.SoundPlayer)
- Visual Studio 2022/2026, or any C#IDE
- Git for version control

# Installation
Clone the repository:
git clone https://github.com/ElomeGA04/CyberChatbot.git
cd CyberChatbot

Running the Part 1 Console Application:
cd CyberChatbot
dotnet run

Running the Part 2 WPF Application:
cd CyberChatbotGUI
dotnet run
-Alternatively, open CyberChatbotGUI.csproj in Visual Studio and press F5

# How to use
1. When the application starts, the voice greeting plays, and the ASCII art logo appears in the header
2. Enter your name when prompted.
3. Ask questions about any of the following topics:
   - password
   - phishing
   - safe browsing
   - scam
   - privacy
4. Use the following commands to interact further:
   - **tell me more** Continues the last topic with a new response
   - **I'm interested in [topic]** Saves the topic to memory
   - **what am I interested in** Recalls the saved topic
   - **what is my name** Recalls the saved name
   - **I'm worried about [topic]** Triggers an empathetic response
   - **exit** Closes the conversation

# Chatbot logic
The chatbot uses a layered approach to generate responses:
1. Sentiment detection: Runs first and produces an empathetic prefix if the user's input contains emotional keywords.
2. Conversation flow: Checks for follow-up commands such as "tell me more" and continues the previous topic.
3. Memory recall: Checks whether the user is asking for information the bot has saved, such as their name or favourite topic.
4. Memory storage: Saves new information when the user expresses interest in a topic
5. Keyword recognition: Scans the input for known cybersecurity topics and returns a random response from that topic's list.
6. Fallback: Provides a helpful message with topic suggestions if no match is found.
Topics are stored in a **Dictionary<string, List<string>>** to allow for multiple responses per topic and fast keyword lookup. A single **Random** instance is used for all random selections.

# Continuous integration
This repository uses GitHub Actions for Continuous integration. The workflow is defined in **.github/workflows/ci.yml** and runs on every push and pull request to the **master** branch.
The workflow performs the following steps:
1. Checks out the repository code
2. Sets up the .NET SDK (version 10.0.x)
3. Restores NuGet dependencies (dotnet restore)
4. Builds the project in Release configuration (dotnet build --configuration Release)
5. Checks for syntax errors in Debug configuration
The CI pipeline ensures the project remains in a buildable state at all times.

# Releases
- v1.0 - Part 1: Console chatbot with voice greeting, ASCII art, and basic keyword responses
- v2.0 - Part 2: WPF GUI chatbot with sentiment detection, memory recall, and random responses

# Video presentation: Unlisted YouTube Link
- Part 1 - Console Chatbot: https://youtu.be/bAEvqBl0ziE
- Part 2 - GUI Chatbot: https://youtu.be/sdLOyV6P7mY

The Part 1 video demonstrates:

1. Code structure and class relationships between Program.cs, ResponseHandler.cs, and AudioPlayer.cs
2. Voice greeting integration using System.Media.SoundPlayer
3. Console UI formatting techniques, including coloured text, borders, and the typing effect
4. Input validation logic for empty entries and unsupported queries
5. CI pipeline setup and a successful run using GitHub Actions
6. The chatbot's responses to various cybersecurity topics

The Part 2 video covers:
1. Live demonstration of the GUI, voice greeting, and chat flow
2. Code structure and class responsibilities
3. Sentiment detection and memory recall in action
4. GitHub commit history, CI workflow, and releases

# References
Microsoft Corporation (2026) System.Media Namespace. 
Available at: https://docs.microsoft.com/en-us/dotnet/api/system.media (Accessed: 9 September 2026).
Microsoft Corporation (2026) Console Class in C# . 
Available at: https://docs.microsoft.com/en-us/dotnet/api/system.console (Accessed: 9 September 2026).
Stack Overflow (2026) How to add a typing effect in C#console application. 
Available at: https://stackoverflow.com/questions/[link] (Accessed: 9 September 2026).
The Independent Institute of Education (2026) Programming 2A: PROG6221/w Study Guide. Cape Town: IIE.
GitHub, 2026. GitHub Actions Documentation. Available at: https://docs.github.com/en/actions (Accessed: 15 September 2026).
Microsoft Corporation, 2026. System.Media Namespace. Available at: https://docs.microsoft.com/en-us/dotnet/api/system.media (Accessed: 15 September 2026).
Microsoft Corporation, 2026. Windows Presentation Foundation (WPF). Available at: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/ (Accessed: 15 September 2026).
The Independent Institute of Education, 2026. Programming 2A: PROG6221/w Study Guide. Cape Town: IIE.
