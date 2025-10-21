/*
BAD: Violates SRP - One class doing too many unrelated things
*/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class UserManager
{
    /// <summary>
    /// This class does EVERYTHING - validation, DB, email, reporting
    /// </summary>
    private Dictionary<int, Dictionary<string, string>> users;
    private int nextId;

    public UserManager()
    {
        users = new Dictionary<int, Dictionary<string, string>>();
        nextId = 1;
    }

    public int CreateUser(string email, string password, string name)
    {
        // Validation logic
        if (!Regex.IsMatch(email, @"[^@]+@[^@]+\.[^@]+"))
        {
            throw new ArgumentException("Invalid email");
        }
        if (password.Length < 8)
        {
            throw new ArgumentException("Password too short");
        }

        // Database logic
        int userId = nextId++;
        users[userId] = new Dictionary<string, string>
        {
            { "email", email },
            { "password", password },
            { "name", name }
        };

        // Email sending logic
        SendWelcomeEmail(email, name);

        // Reporting logic
        LogUserCreation(userId, email);

        return userId;
    }

    private void SendWelcomeEmail(string email, string name)
    {
        /// <summary>
        /// Email sending mixed with user management
        /// </summary>
        Console.WriteLine($"Sending email to {email}...");
        // Imagine SMTP code here
        Console.WriteLine($"Welcome {name}!");
    }

    private void LogUserCreation(int userId, string email)
    {
        /// <summary>
        /// Reporting mixed with user management
        /// </summary>
        Console.WriteLine($"LOG: User {userId} created with email {email}");
    }

    public string GenerateUserReport()
    {
        /// <summary>
        /// Even more unrelated responsibility!
        /// </summary>
        string report = $"Total users: {users.Count}\n";
        foreach (var kvp in users)
        {
            report += $"  {kvp.Key}: {kvp.Value["email"]}\n";
        }
        return report;
    }

    public static void Main()
    {
        // Problem: Hard to test just validation, or swap email provider, or
        // change DB
        var manager = new UserManager();

        int userId = manager.CreateUser(
            "alice@example.com",
            "password123",
            "Alice"
        );
        Console.WriteLine($"Created user: {userId}");

        Console.WriteLine("\n" + manager.GenerateUserReport());

        Console.WriteLine("\n[X] PROBLEMS:");
        Console.WriteLine("- Can't test validation without triggering emails");
        Console.WriteLine("- Can't change database without touching email code");
        Console.WriteLine("- Can't reuse email sender for other purposes");
        Console.WriteLine("- 4+ reasons for this class to change!");
    }
}