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

        ///MEMORY: Stores information the user shares during the conversation
        ///The user's name, captured on first input.
        public string UserName { get; set; } = "";

        /// The topic the user said they are interested in.
        public string FavouriteTopic { get; set; } = "";

        /// Most recently discussed topic, used for 'tell me more'.
        private string _lastTopic = "";


        ///RESPONSE DATA
        ///Random response generator
        private readonly Dictionary<string, List<string>> _topicResponses;

        ///Generator
        private readonly Random _random = new Random();

        /// Initializes the topic and sentiment response dictionaries.
        public ChatbotEngine()
        {
            ///CYBERSECURITY TOPIC RESPONSES
            _topicResponses = new Dictionary<string, List<string>>()
            {
                {"password", new List<string>
                    {
                        "Treat passwords like keys to different doors: If someone copies the key to your email, they can unlock everything linked to it. Unique passwords mean a breach on one site never compromizes the other. A password manager can generate and remember them all, you only need to secure one master password.",
                        "Length beats complexity. A 16-character passphrase like 'BlueRiverCoffeeTable@7' is far stronger than 'P@ssw0rd' and much easier to remember. Aim for at least 12 characters mixing case, numbers, and symbols.",
                        "Enable two-factor authentication (2FA) wherever it is offered. Even if an attacker steals your password, they still need the second factor, usually a code from your phone to log in. This single step blocks the vast majority of account takeovers.",
                        "Watch out for password reuse across your email, banking, and social media. Cybercriminals buy leaked credential lists and try them everywhere (credential stuffing). If one match works, they drain accounts. Use a unique password for each of these critical services at minimum.",
                        "Never share your password via email, SMS, or chat even if the request appears to come from someone you trust. Legitimate companies never ask for your password. If you suspect an account is compromised, change the password immediately and enable 2FA."

                    }
                },
                {"phishing", new List<string>
                    {
                        "Phishing works by impersonation. Attackers mimic your bank, courier, or employer to trick you into clicking a link or handing over credentials. Before acting, hover over the link to reveal the real URL, if it does not match the official domain exactly, do not click.",
                        "Red flags to watch for: urgent language ('Your account will be closed in 24 hours'), generic greetings ('Dear Customer'), mismatched sender addresses, and unexpected attachments. Real organisations do not pressure you via email, they use secure portals.",
                        "If you receive a suspicious email, do not reply, click links, or open attachments. Report it as phishing in your email client, then delete it. Reporting helps your provider block the same attack for others.",
                        "Smishing (SMS phishing) and vishing (voice phishing) are on the rise. A text claiming your parcel is 'held at customs' or a call from 'Microsoft support' about a virus is almost always fake. Hang up and call the organization back using a number you look up yourself.",
                        "Even one click can be costly. If you clicked a phishing link, immediately change the passwords for any accounts you entered credentials into, enable 2FA, and check your bank statements for unusual activity. Time is critical, act within minutes."

                    }
                },
                {"safe browsing", new List<string>
                    {
                        "Look for the padlock icon and 'https://' in the address bar before entering any personal details. The 's' stands for secure, it encrypts traffic between your browser and the site. Never enter card details on a site that only uses 'http://'.",
                        "Public Wi-Fi in cafes, airports, and hotels is convenient but risky, anyone on the same network can potentially intercept your traffic. Use a reputable VPN, or stick to mobile data, when logging into banking or email.",
                        "Keep your browser and operating system updated. Most successful attacks exploit known vulnerabilities that were patched months ago. Enable automatic updates so you are never running outdated software.",
                        "Install an ad-blocker and a reputable antivirus. Malicious ads (malvertising) can infect your device even without clicking, they exploit flaws in ad networks. Blocking ads closes a common infection route.",
                        "Think before you click. Hover over links to preview the destination, avoid pop-ups claiming your device is infected, and never download software from unofficial sources. If a site pressures you to install something, close the tab."
                    }

                },
                {"scam", new List<string>
                    {
                        "If an offer sounds too good to be true, it is. Scams rely on urgency and emotion, a lottery you never entered, an investment that doubles your money, a refund you were not expecting. Pause, verify independently, and never act in the moment.",
                        "Tech support scams start with a pop-up or cold call claiming your computer is infected. Real companies like Microsoft never call you out of the blue. Hang up, close the pop-up, and run your own antivirus scan.",
                        "Investment scams promise guaranteed returns, a red flag in itself, since all investments carry risk. Check that any advisor is registered with your country's financial regulator before handing over money. If they pressure you to act fast, walk away.",
                        "If you suspect a scam, report it to your bank, your country's fraud reporting service, and the platform where it occurred. Reporting helps protect others, and banks can sometimes recover funds if you act quickly."
                    }
                },
                {"privacy", new List<string>
                    {
                        "Your digital footprint lasts longer than you think. Review your social media privacy settings every few months, set posts to 'friends only', disable location tagging, and remove old accounts you no longer use. Data you share today can surface years later.",
                        "Read privacy policies, at least the summary. Look for what data an app collects, who it shares it with, and whether it sells it to third parties. If an app asks for permissions it does not need (a torch app asking for contacts), deny them.",
                        "Use a VPN on public Wi-Fi to encrypt your browsing from eavesdroppers on the same network. Free VPNs often sell your data, pay for a reputable one, or use your mobile hotspot instead.",
                        "Be cautious with 'Sign in with Google/Facebook' buttons. They are convenient but give the third-party app access to your profile data and sometimes your contacts. Use a unique email for sign-ups where possible.",
                    }
                }
            };
        }

        /// MAIN ENTRY POINT
        /// Processes the user's input and returns an appropriate reply.
        public string GetResponse(string userInput)
        {
            ///Normalizes input: lovercase + trim for case- insensitive matching
            string question = userInput.ToLower().Trim();

            ///Detects emotional tone and build a tonal prefix for the reply
            string sentiment = DetectSentiment(question);
            string prefix = GetSentimentPrefix(sentiment);
            if (prefix != "") prefix += "\n\n";

            /// CONVERSATIONAL FLOW: "tell me more"
            /// Continue th eprevious topic instead of starting a new one.
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

            ///GENERAL CONVERSATION
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

            ///MEMORY RECALL: user asks for their name
            if(question.Contains("what is my name") || question.Contains("who am i") || question.Contains("my name"))
            {
                return $"Your name is {UserName} of course.";
            }

            ///MEMORY RECALL: user asks about their interest
            if(question.Contains("what am i interested in") || question.Contains("what do i like") || 
                question.Contains("my favourite") || question.Contains("what do you remember"))
            {
                if (!string.IsNullOrEmpty(FavouriteTopic))
                    return $"You are interested in {FavouriteTopic}, {UserName}. I will keep that in mind when I give you tips.";
                return $"You haven't told me your favourite topic yet. Try saying 'I'm interested in privacy'.";
            }

            ///MEMORY STORE: user expresses interest in a topic
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

            ///KEYWORD RECOGNITION
            ///Scan the dictionary keys and match the first keyword found.
            foreach (var topic in _topicResponses.Keys)
            {
                if (question.Contains(topic))
                {
                    _lastTopic = topic;
                    return prefix + $"{UserName}, {GetRandomResponse(topic)}";
                }
            }

            ///MEMORY-BASED TIP (user asked for help or advice)
            if (!string.IsNullOrEmpty(FavouriteTopic) &&
                (question.Contains("tip") ||
                question.Contains("advice") ||
                question.Contains("help")))
            {
                return $"As someone interested in {FavouriteTopic}, " +
                    $"here is a tip: {GetRandomResponse(FavouriteTopic)}";
            }

            ///SENTIMENT-ONLY REPLY
            ///User expressed emotion but no topic, auto-share a random tip.
            if(sentiment != "neutral")
            {
                var topics = new List<string>(_topicResponses.Keys);
                string randomTopic = topics[_random.Next(topics.Count)];
                _lastTopic = randomTopic;
                return prefix + $"Here is a tip about {randomTopic}: {GetRandomResponse(randomTopic)}";
            }

            ///FALLBACK
            return $"Sorry {UserName}, I didn't quite understand that. Could you rephrase it? Try asking about passwords, phishing, safe browsing, scam, or privacy.";

        }

        /// HELPER METHODS
        /// Scans the user's input for emotional keywords and 
        /// returns the detected mood: "worried", "curious", "frustrated", or "neutral".
        private string DetectSentiment(string input)
        {
            ///Worried: fear, anxienty, unease
            if (input.Contains("worried") || input.Contains("scared") ||
                input.Contains("nervous") || input.Contains("anxious") ||
                input.Contains("afraid"))
            {
                return "worried";
            }

            ///Curious: interest, questioning
            if (input.Contains("curious") || input.Contains("wondering")) 
                
            {
                return "curious";
            }

            ///Frustrated: annoyance, confusion, stress
            if (input.Contains("frustrated") || input.Contains("annoyed") ||
                input.Contains("confused") || input.Contains("angry") ||
                input.Contains("stressed"))
            {
                return "frustrated";
            }

            return "neutral";
        }

        /// SENTIMENT RESPONSES
        /// Multiple empathetic prefixes per mood produce natural variation
        
        private string GetSentimentPrefix(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "It's completely understandable to feel that way, cyber threats are real and can be intimidating. Let me share something practical to help you feel more in control. You're not alone in feeling concerned. Millions of people face these threats every day, and the good news is that the right precautions make a huge difference. Here is a tip that can help.";
                case "curious":
                    return "That's a great question! Here is some helpful information.";
                case "frustrated":
                    return "I understand your frustration cybersecurity can feel overwhelming when you're just trying to get things done. Technology shouldn't make life harder, and these tips are designed to be quick and easy. Let's take it one step at a time with something simple.";
                default:
                    return "";

            }
        }

        /// Returns a random response from the given topic's list.
        private string GetRandomResponse(string topic)
        {
            List<string> responses = _topicResponses[topic];
            return responses[_random.Next(responses.Count)];


        }

    }
}
