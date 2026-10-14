using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChatbotGUI
{
    /// <summary>
    /// The ChatbotEngine class is intended to encapsulate the core logic and functionality of the CyberChatbot application.
    /// The brain of the chatbot, it will handle user input, generate responses, and manage the conversation flow.
    /// </summary>
    internal class ChatbotEngine
    {

        ///Memory 
        public string UserName { get; set; } = "";
        public string FavouriteTopic { get; set; } = "";
        private string _lastTopic = "";

        ///Random response generator
        private readonly Dictionary<string, List<string>> _topicResponses;

        ///Generator
        private readonly Random _random = new Random();

        public ChatbotEngine()
        {
            _topicResponses = new Dictionary<string, List<string>>()
            {
                {"password", new List<string>
                    {
                        "Use strong, unique passwords for each account and consider using a password manager.",
                        "Never reuse passwords across different sites, if one gets breached, they all do.",
                        "Combine upper and lower case letters, numbers, and symbols to strengthen your passwords."
                    }
                },
                {"phishing", new List<string>
                    {
                        "Be cautious of suspicious emails asking for personal information. Verify the sender before clicking any links.",
                        "Phishing emails often mimic trusted organazations. Check the email address carefully before responding.",
                        "Never enter sensitive information on websites you reached through email links. Always navigate directly to the site.",

                    }
                },
                {"safe browsing", new List<string>
                    {
                        "Use secure websites (https), avoid clicking on unknown links, and keep your browser updated.",
                        "Install a reputable ad-blocker to reduce exposure to malicious ads.",
                        "Avoid using public WiFi for sensitive tasks unless you are on a VPN."
                    }

                },
                {"scam", new List<string>
                    {
                        "If an offer sounds too good to be true, it probably is. Scammers rely on urgency to make you act fast.",
                        "Never send money or gift cards to someone you have not met in person or simply people you do not know.",
                        "If you suspect a scam, report it to your bank and the relevent authorities immediately."
                    }
                },
                {"privacy", new List<string>
                    {
                        "Review your social media privacy settings regularly to control who sees your data.",
                        "Use a VPN on public WiFi to protect your browsing from eavesdroppers.",
                        "Be mindful of what personal information you share online, once it is out there, it's hard to remove."
                    }
                }
            };
        }

        public string GetResponse(string userInput)
        {
            string question = userInput.ToLower().Trim();

            if(question.Contains("tell me more") ||
               question.Contains("explain more") ||
               question.Contains("another tip") ||
               question.Contains("more info"))
            {
                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    return GetRandomResponse(_lastTopic);
                }
                else
                {
                    return "What topic would you like to know more about? (e.g. password, phishing, safe browsing)";
                }
            }

            if(question.Contains("how are you"))
            {
                return $"I am doing well {UserName}, and ready to help you stay safe online.";

            }



        }

    }
}
