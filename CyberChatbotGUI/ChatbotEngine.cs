using System;
using System.Collections.Generic;

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
                        "Treat passwords like keys to different doors: If someone copies the key to your email, " +
                        "they can unlock everything linked to it. Unique passwords mean a breach on one site never compromizes the other. " +
                        "A password manager can generate and remember them all, you only need to secure one master password.",
                        "Length beats complexity. A 16-character passphrase like 'BlueRiverCoffeeTable@7' is far stronger than 'P@ssw0rd' and much easier to remember. Aim for at least 12 characters mixing case, numbers, and symbols.",

                    }
                },
                {"phishing", new List<string>
                    {
                        "Be cautious of suspicious emails asking for personal information. Verify the sender before clicking any links.",
                        "Phishing emails often mimic trusted organizations. Check the email address carefully before responding.",
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
                        "If you suspect a scam, report it to your bank and the relevant authorities immediately."
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

            string sentiment = DetectSentiment(question);
            string prefix = GetSentimentPrefix(sentiment);

            if (question.Contains("tell me more") ||
               question.Contains("explain more") ||
               question.Contains("another tip") ||
               question.Contains("more info"))
            {
                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    return prefix + GetRandomResponse(_lastTopic);
                }
                else
                {
                    return "What topic would you like to know more about? (e.g. password, phishing, safe browsing)";
                }
            }

            if (question.Contains("how are you"))
            {
                return prefix + $"I am doing well {UserName}, and ready to help you stay safe online.";

            }
            if (question.Contains("purpose"))
            {
                return $"My purpose is to assist you with cyber safety information and guidance, {UserName}.";

            }
            if (question.Contains("what can i ask you") || question.Contains("topic"))
            {
                return $"{UserName}, you can ask about passwords, phishing, safe browsing, scams, or privacy.";
            }

            if(question.Contains("what is my name") || question.Contains("who am i") || question.Contains("my name"))
            {
                return $"Your name is {UserName} of course.";
            }

            if(question.Contains("what am i interested in") || question.Contains("what do i like") || 
                question.Contains("my favourite") || question.Contains("what do you remember"))
            {
                if (!string.IsNullOrEmpty(FavouriteTopic))
                    return $"You are interested in {FavouriteTopic}, {UserName}. I will keep that in mind when I give you tips.";
                return $"You haven't told me your favourite topic yet. Try saying 'I'm interested in privacy'.";
            }

            if (question.Contains("interested in") || question.Contains("i like"))
            {
                foreach (var topic in _topicResponses.Keys)
                {
                    if (question.Contains(topic))
                    {
                        FavouriteTopic = topic;
                        _lastTopic = topic;
                        return $"Great! I'll remember that you are interested in {topic}. It's a crucial part of staying safe online.";
                    }
                }
            }

            foreach (var topic in _topicResponses.Keys)
            {
                if (question.Contains(topic))
                {
                    _lastTopic = topic;
                    return prefix + $"{UserName}, {GetRandomResponse(topic)}";
                }
            }

            if (!string.IsNullOrEmpty(FavouriteTopic) &&
                (question.Contains("tip") ||
                question.Contains("advice") ||
                question.Contains("help")))
            {
                return $"As someone interested in {FavouriteTopic}, " +
                    $"here is a tip: {GetRandomResponse(FavouriteTopic)}";
            }

            if(sentiment != "neutral")
            {
                var topics = new List<string>(_topicResponses.Keys);
                string randomTopic = topics[_random.Next(topics.Count)];
                _lastTopic = randomTopic;
                return prefix + $"Here is a tip about {randomTopic}: {GetRandomResponse(randomTopic)}";
            }

            return $"Sorry {UserName}, I didn't quite understand that. Could you rephrase it? Try asking about passwords, phishing, safe browsing, scam, or privacy.";

        }
        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") ||
                input.Contains("nervous") || input.Contains("anxious") ||
                input.Contains("afraid"))
            {
                return "worried";
            }

            if (input.Contains("curious") || input.Contains("wondering")) 
                
            {
                return "curious";
            }

            if (input.Contains("frustrated") || input.Contains("annoyed") ||
                input.Contains("confused") || input.Contains("angry") ||
                input.Contains("stressed"))
            {
                return "frustrated";
            }

            return "neutral";
        }

        private string GetSentimentPrefix(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "It's understandable to feel that way. Scammers can be very convincing.";
                case "curious":
                    return "That's a great question! Here is some helpful information.";
                case "frustrated":
                    return "I understand your frustration. Let's take this one step at a time.";
                default:
                    return "";

            }
        }

        private string GetRandomResponse(string topic)
        {
            List<string> responses = _topicResponses[topic];
            return responses[_random.Next(responses.Count)];


        }

    }
}
