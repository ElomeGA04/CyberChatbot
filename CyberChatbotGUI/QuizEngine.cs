using System;
using System.Collections.Generic;

namespace CyberChatbotGUI
{
    public class QuizEngine
    {
        public List<CyberQuizQuestions> Questions { get; private set; }
        public int CurrentQuestionIndex { get; private set; } = 0;
        public int Score { get; private set; } = 0;

        public QuizEngine()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            Questions = new List<CyberQuizQuestions>
            {
                new CyberQuizQuestions {QuestionText = "True or False: It is safe to use the same password for multiple accounts.",
                                        OptionA = "True",
                                        OptionB = "False",
                                        OptionC = "",
                                        OptionD = "",
                                        CorrectAnswer = "B",
                                        Explanation = "If onw account is breached, hackers will try that password on all your other accounts (credential stuffing)."},
                new CyberQuizQuestions {QuestionText = "What is the strongest password?",
                                        OptionA = "123456",
                                        OptionB =  "Password123",
                                        OptionC = " BlueTable!7River",
                                        OptionD = "Your birthdate",
                                        CorrectAnswer = "C",
                                        Explanation = "Long passphrases with symbols and mixed cases are the strongest."},
                new CyberQuizQuestions {QuestionText = "What does 'Phishing' mean?",
                                        OptionA = "A type of fishing",
                                        OptionB = "A cyber attack that tricks you into giving personal information",
                                        OptionC = "A way to protect your data",
                                        OptionD = "A type of computer virus",
                                        CorrectAnswer = "B",
                                        Explanation = "Phishing is a cyber attack where attackers impersonate legitimate entities to steal sensitive information."},
                new CyberQuizQuestions {QuestionText = "What is the best way to protect your online accounts?",
                                        OptionA = "Use strong, unique passwords for each account",
                                        OptionB = "Share your passwords with friends",
                                        OptionC = "Write your passwords on a sticky note",
                                        OptionD = "Use the same password for all accounts",
                                        CorrectAnswer = "A",
                                        Explanation = "Using strong, unique passwords for each account helps prevent unauthorized access."},
                new CyberQuizQuestions {QuestionText = "Which of these is a red flag of a scam email?",
                                        OptionA = "Urgent language like 'Act Now!!!",
                                        OptionB = "The email contains spelling errors and urgent requests",
                                        OptionC = "A mismatch in the sender's email address",
                                        OptionD = "All of the above",
                                        CorrectAnswer = "D",
                                        Explanation = "Scam emails often contain spelling errors, urgent requests, and ask for personal information." },
                new CyberQuizQuestions {QuestionText = "True or False: Public Wi-Fi is always secure.",
                                        OptionA = "True",
                                        OptionB = "False",
                                        OptionC = "",
                                        OptionD = "",
                                        CorrectAnswer = "B",
                                        Explanation = "Public Wi-Fi networks can be insecure, making it easier for attackers to intercept your data. Use a VPN to protect your data."},
                new CyberQuizQuestions {QuestionText = "What is Two-Factor Authentication (2FA)?",
                                        OptionA = "Using two passwords",
                                        OptionB = "A second layer of security (like a phone code)",
                                        OptionC = "Logging in twice",
                                        OptionD = "Having two email accoounts",
                                        CorrectAnswer = "B",
                                        Explanation = "2FA requires a second proof of identity, blocking hackers even if they have your password."},

                new CyberQuizQuestions {QuestionText = "What does the 'S' in 'HTTPS' stand for?",
                                        OptionA = "Secure",
                                        OptionB = "Server",
                                        OptionC = "Simple",
                                        OptionD = "System",
                                        CorrectAnswer = "A",
                                        Explanation = "The 'S' in HTTPS stands for 'Secure', indicating that the connection is encrypted and secure. HTTPS means the traffic between your browser and the website is encrypted."},
                new CyberQuizQuestions {QuestionText = "True or False: You should click links in unexpected emails to verify if they are real.",
                                        OptionA = "True",
                                        OptionB = "False",
                                        OptionC = "",
                                        OptionD = "",
                                        CorrectAnswer = "B",
                                        Explanation = "Clicking links in unexpected emails can lead to phishing attacks. Always verify the sender and hover over them to check the real URL, or navigate to the site directly."},
                new CyberQuizQuestions {QuestionText = "Which of these is a form of social engineering?",
                                        OptionA = "Writing a virus",
                                        OptionB = "Calling someone pretending to be IT support",
                                        OptionC = "Brute-forcing a password",
                                        OptionD = "Using a keylogger",
                                        CorrectAnswer = "B",
                                        Explanation = "Social engineering is a manipulation technique that exploits human error to gain private information. Phishing, pretexting, and baiting are forms of social engineering."},
                new CyberQuizQuestions {QuestionText = "How often should you update your software?",
                                        OptionA = "Only when you notice a problem",
                                        OptionB = "Once a year",
                                        OptionC = "Regularly, as soon as updates are available",
                                        OptionD = "Never",
                                        CorrectAnswer = "C",
                                        Explanation = "Regularly updating your software ensures you have the latest security patches, protecting against vulnerabilities."},
                new CyberQuizQuestions {QuestionText = "True or False: Antivirus software alone is enough to keep you safe.",
                                        OptionA = "True",
                                        OptionB = "False",
                                        OptionC = "",
                                        OptionD = "",
                                        CorrectAnswer = "B",
                                        Explanation = "While antivirus software is important, it should be part of a multi-layered security approach that includes strong passwords, 2FA, and safe browsing habits."},
                new CyberQuizQuestions {QuestionText = "What should you do if you suspect an account is compromised?",
                                        OptionA = "Change the password immediately and enable 2FA",
                                        OptionB = "Ignore it and continue using the account",
                                        OptionC = "Tell your friends about it",
                                        OptionD = "Wait for a notification",
                                        CorrectAnswer = "A",
                                        Explanation = "If you suspect an account is compromised, change the password immediately and enable two-factor authentication if available."},
                new CyberQuizQuestions {QuestionText = "What is a VPN and why is it important?",
                                        OptionA = "A virtual private network that encrypts your internet traffic",
                                        OptionB = "A type of firewall",
                                        OptionC = "A software that detects malware",
                                        OptionD = "A method of storing data securely",
                                        CorrectAnswer = "A",
                                        Explanation = "A VPN (Virtual Private Network) creates a secure, encrypted connection over a less secure network, such as the internet. It helps protect your privacy and security by masking your IP address and encrypting your data."}

            };
        }

        public CyberQuizQuestions GetCurrentQuestion()
        {
            return Questions[CurrentQuestionIndex];
        }

        //Returns true if the answer was correct
        public bool SubmitAnswer(string selectedOption)
        {
            bool isCorrect = (selectedOption == Questions[CurrentQuestionIndex].CorrectAnswer);
            if (isCorrect) Score++;
            return isCorrect;
        }

        //Moves to the next question. Returns false if there is no more questions
        public bool MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
            return CurrentQuestionIndex < Questions.Count;
        }

        public string GetFinalMessage()
        {
            double percentage = (double)Score / Questions.Count * 100;
            if (percentage >= 80) return "Great job! You're a cybersecurity expert!";
            if (percentage >= 50) return "Good effort! You have a decent understanding of cybersecurity, but keep learning to stay safe online.";
            return "Keep learning and improving your cybersecurity knowledge!";
        }
    }
}
