/*
GOOD: Each class has a single, clear responsibility
*/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GoodSRP
{
    /// <summary>
    /// Single responsibility: Validate email addresses
    /// </summary>
    public static class EmailValidator
    {
        public static bool Validate(string email)
        {
            return Regex.IsMatch(email, @"[^@]+@[^@]+\.[^@]+");
        }
    }

    /// <summary>
    /// Single responsibility: Validate passwords
    /// </summary>
    public static class PasswordValidator
    {
        public static bool Validate(string password, int minLength = 8)
        {
            return password.Length >= minLength;
        }
    }

    /// <summary>
    /// Single responsibility: Store and retrieve users
    /// </summary>
    public class UserRepository
    {
        private Dictionary<int, Dictionary<string, string>> _users;
        private int _nextId;

        public UserRepository()
        {
            _users = new Dictionary<int, Dictionary<string, string>>();
            _nextId = 1;
        }

        public int Save(string email, string password, string name)
        {
            int userId = _nextId++;
            _users[userId] = new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "name", name }
            };
            return userId;
        }

        public Dictionary<int, Dictionary<string, string>> GetAll()
        {
            return new Dictionary<int, Dictionary<string, string>>(_users);
        }
    }

    /// <summary>
    /// Single responsibility: Send emails
    /// </summary>
    public class EmailService
    {
        public void SendWelcomeEmail(string email, string name)
        {
            Console.WriteLine($"[EMAIL] Sending welcome email to {email}");
            Console.WriteLine($"   Welcome aboard, {name}!");
        }
    }

    /// <summary>
    /// Single responsibility: Log user activities
    /// </summary>
    public class UserActivityLogger
    {
        public void LogCreation(int userId, string email)
        {
            Console.WriteLine($"[LOG] User {userId} created with email {email}");
        }
    }

    /// <summary>
    /// Orchestrates user creation - delegates to specialized services
    /// </summary>
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly EmailService _emailService;
        private readonly UserActivityLogger _logger;

        public UserService(
            UserRepository repository,
            EmailService emailService,
            UserActivityLogger logger
        )
        {
            _repository = repository;
            _emailService = emailService;
            _logger = logger;
        }

        public int CreateUser(string email, string password, string name)
        {
            // Validate
            if (!EmailValidator.Validate(email))
            {
                throw new ArgumentException("Invalid email");
            }
            if (!PasswordValidator.Validate(password))
            {
                throw new ArgumentException("Password too short");
            }

            // Save
            int userId = _repository.Save(email, password, name);

            // Notify
            _emailService.SendWelcomeEmail(email, name);
            _logger.LogCreation(userId, email);

            return userId;
        }
    }

    /// <summary>
    /// Single responsibility: Generate user reports
    /// </summary>
    public class UserReportGenerator
    {
        private readonly UserRepository _repository;

        public UserReportGenerator(UserRepository repository)
        {
            _repository = repository;
        }

        public string GenerateSummary()
        {
            var users = _repository.GetAll();
            string report = $"Total users: {users.Count}\n";
            foreach (var kvp in users)
            {
                report += $"  {kvp.Key}: {kvp.Value["email"]}\n";
            }
            return report;
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Setup - each component can be tested/replaced independently
            var repository = new UserRepository();
            var emailService = new EmailService();
            var logger = new UserActivityLogger();

            var userService = new UserService(repository, emailService, logger);
            var reportGen = new UserReportGenerator(repository);

            // Use
            int userId = userService.CreateUser(
                "alice@example.com",
                "password123",
                "Alice"
            );
            Console.WriteLine($"[OK] Created user: {userId}\n");

            Console.WriteLine(reportGen.GenerateSummary());

            Console.WriteLine("\n[OK] BENEFITS:");
            Console.WriteLine("- Each class has ONE reason to change");
            Console.WriteLine("- Easy to test validators independently");
            Console.WriteLine("- Can swap email service without touching other code");
            Console.WriteLine("- Can reuse components in other contexts");
        }
    }
}