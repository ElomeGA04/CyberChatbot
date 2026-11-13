using System;
using System.Collections.Generic;
using System.Text;

namespace CyberChatbotGUI
{
    public class ActivityLog
    {
        //List to store all actions
        private readonly List<string> _logEntries = new List<string>();

        //Add a new action to the log
        public void AddEntry(string actionDescription)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            _logEntries.Add($"[{timestamp}] {actionDescription}");
        }

        //Get the last 5-10 actions to show the user
        public string GetRecentLog()
        {
            if (_logEntries.Count == 0) return "There are no recent actions to show.";

            //Get thelast 10 actions (or fewer if the list is short)
            var recent = _logEntries.Skip(Math.Max(0, _logEntries.Count - 10)).ToList();
            string result = "Here is a summary of recent actions:\n\n";
            foreach (var entry in recent)
            {
                result += $"- {entry}\n";
            }
            return result;
        }
    }
}
