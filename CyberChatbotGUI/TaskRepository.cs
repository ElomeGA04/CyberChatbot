using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MySqlConnector;

namespace CyberChatbotGUI
{
    public class TaskRepository
    {
        private const string ConnectionString = "Server=localhost;Database=cyberchatbot;User ID=root;Password=Emole@110403@.;";

        public async Task AddTaskAsync(SecurityTask task)
        {
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();
            string query = "INSERT INTO security_tasks (title, description, reminder_date, is_completed) VALUES (@title, @desc, @reminder, @completed)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@desc", task.Description ?? "");
            cmd.Parameters.AddWithValue("@reminder", task.ReminderDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@completed", task.IsCompleted); 
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<ObservableCollection<SecurityTask>> GetAllTasksAsync()
        {
            var tasks = new ObservableCollection<SecurityTask>();
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();
            string query = "SELECT * FROM security_tasks ORDER BY is_completed ASC, reminder_date ASC";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tasks.Add(new SecurityTask
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                    ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? null : reader.GetDateTime("reminder_date"),
                    IsCompleted = reader.GetBoolean("is_completed")
                }); 
            }

            return tasks;

        }

        public async Task MarkCompleteAsync(int taskId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();
            string query = "UPDATE security_tasks SET is_completed = TRUE WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", taskId);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();
            string query = "DELETE FROM security_tasks WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", taskId);
            await cmd.ExecuteNonQueryAsync();
        }

    }
}
