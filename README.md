# CyberChatbot: Cybersecurity Awareness Bot
 
 # Description
 The Cybersercurity Awareness Bot is a console-based chatbot written in C#(.NET) that educates users about basic cybersecurity practices in an interactive, coversational manner. 
 The bot greets the user with a recorded voice message, displays a cybersecurity-themed ASCII art image, asks the user's name to personalize the conversation, 
 and answers questions about topics such as password safety, phishing, and safe browsing.

 The application demonstrates core programming concepts including input validation, 
 string handling, control flow, methods, classes, console formatting (colors, borders, dividers), 
 audio playback with System.Media, and a typing effect to simulate natural conversation.

 # Features
Voice greeting:
Plays a recorded WAV audio message (greeting.wav) welcoming the user when the application launches.

ASCII Art Image:
Displays a "Cybersecurity Awareness Bot" ASCII art title screen (The console window must be zoomed in/ minimized to see the full ASCII art properly.)

Personalized greeting:
Asks the user for their name and uses it throughout the conversation.

Basic response system:
Answers questions such as "How are you?", "What is your purpose?", and "What can I ask you about?"

Cybersecurity topics:
Provides responses on password safety, phishing, and safe browsing.

Input validation:
Detects empty or unsupported input and responds gracefully: "I didn't quite understand that. Could you rephrase?"

Enhanced console UI:
Uses colored text, decorative borders, section headers, and dividers for a structured interface.

Typing effect:
Simulates a conversational feel with slight delays when the bot types bot types its responses.

Graceful exit:
Typing in 'exit' ends the chat with a personalized goodbye message.

# Requirements
- .NET SDK 6.0 or higher
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
3. Or open Visual Studio > Open project > Select CyberChatbot.csproj

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
Main application entry point. Handles UI, color formatting, typing effect, and user interaction loop.

AudioPayer.cs
Plays the welcome voice message using System.Media.SoundPlayer.

ResponseHandler.cs
Contains logic for processing user input and generating appropriate responses based on predefined topics (password safety, phishing, safe browsing).

CyberWelcome.wav
Audio file played when the application starts.

# Continuous Integration (Github Actions)
This repository uses Github Actions for Continuous Integration (CI). On every push the CI workflow automatically:
1. Restores dependencies
2. Builds the project (dotnet build)
3. Checks for syntax errors and formatting/build issues
4. Runs on every push and pull request to the main branch

# Successful CI Run
screenshot/ci-pass.png shows a successful CI run with all steps completed without errors.

# Cybersecurity topics covered
Password safety:
Use strong, unique passwords for each account and consider using a password manager to securely store them.

Phishing:
Be cautious of suspicious emails or messages asking for personal information. Verify the source before clicking any links

Safe browsing:
Use secure websites (HTTPS), avoid clicking on unknown links, and keep your browser updated.

# Video Presentation
Unlisted YouTube link: 

The presentation demonstrates:
1. Code structure and class relationships between Program.cs, ResponseHandler.cs, and AudioPlayer.cs
2. Voice greeting integration using System.Media.SoundPlayer
3. Console UI formatting techniques including colored text, borders, and typing effect
4. Input validation logic for empty entries and unsupported queries
5. CI pipeline setup and successful run using Github Actions
6. Application functionality showcasing the chatbot's responses to various cybersecurity topics

# References
Microsoft Corporation (2026) System.Media Namespace. 
Available at: https://docs.microsoft.com/en-us/dotnet/api/system.media (Accessed: 9 September 2026).
Microsoft Corporation (2026) Console Class in C#. 
Available at: https://docs.microsoft.com/en-us/dotnet/api/system.console (Accessed: 9 September 2026).
Stack Overflow (2026) How to add a typing effect in C# console application. 
Available at: https://stackoverflow.com/questions/[link] (Accessed: 9 September 2026).
The Independent Institute of Education (2026) Programming 2A: PROG6221/w Study Guide. Cape Town: IIE.