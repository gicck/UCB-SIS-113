/*
GOOD: Proper encapsulation and abstraction
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodEncapsulation
{
    /// <summary>
    /// Encapsulates transaction details
    /// </summary>
    public class Transaction
    {
        private readonly decimal _amount;
        private readonly string _type;
        private readonly DateTime _timestamp;
        private readonly string _description;

        public Transaction(
            decimal amount,
            string transactionType,
            string description
        )
        {
            _amount = amount;
            _type = transactionType;
            _timestamp = DateTime.Now;
            _description = description;
        }

        public override string ToString()
        {
            return $"{_timestamp:yyyy-MM-dd HH:mm} | {_type,-10} | " +
                   $"${_amount,8:F2} | {_description}";
        }
    }

    /// <summary>
    /// Encapsulated bank account with controlled access
    /// </summary>
    public class BankAccount
    {
        private readonly string _owner;
        private decimal _balance;  // Private field
        private readonly List<Transaction> _transactions;  // Private field

        public BankAccount(string owner, decimal initialBalance = 0)
        {
            _owner = owner;
            _balance = initialBalance;
            _transactions = new List<Transaction>();

            if (initialBalance > 0)
            {
                AddTransaction(
                    initialBalance,
                    "DEPOSIT",
                    "Initial deposit"
                );
            }
        }

        // Public interface - abstraction of account operations

        /// <summary>
        /// Deposit money - with validation
        /// </summary>
        public decimal Deposit(decimal amount, string description = "Deposit")
        {
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Deposit amount must be positive"
                );
            }

            _balance += amount;
            AddTransaction(amount, "DEPOSIT", description);
            return _balance;
        }

        /// <summary>
        /// Withdraw money - with validation
        /// </summary>
        public decimal Withdraw(
            decimal amount,
            string description = "Withdrawal"
        )
        {
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Withdrawal amount must be positive"
                );
            }
            if (amount > _balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            _balance -= amount;
            AddTransaction(-amount, "WITHDRAWAL", description);
            return _balance;
        }

        /// <summary>
        /// Read-only access to balance
        /// </summary>
        public decimal GetBalance()
        {
            return _balance;
        }

        /// <summary>
        /// Get transaction history - returns copy, not original
        /// </summary>
        public List<string> GetStatement()
        {
            return _transactions.Select(t => t.ToString()).ToList();
        }

        // Private helper methods - implementation details

        private void AddTransaction(
            decimal amount,
            string transType,
            string description
        )
        {
            var transaction = new Transaction(amount, transType, description);
            _transactions.Add(transaction);
        }

        public override string ToString()
        {
            return $"Account({_owner}): ${_balance:F2}";
        }
    }

    public class Program
    {
        public static void Main()
        {
            var account = new BankAccount("Alice", 1000);

            Console.WriteLine($"[OK] Initial: {account}");

            // Proper way: Use public interface
            account.Deposit(500, "Salary");
            Console.WriteLine($"[OK] After deposit: {account}");

            account.Withdraw(200, "Groceries");
            Console.WriteLine($"[OK] After withdrawal: {account}");

            // Try to do something invalid
            try
            {
                account.Withdraw(10000, "Mansion");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"[OK] Validation works: {e.Message}");
            }

            try
            {
                account.Deposit(-100, "Hacking attempt");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"[OK] Validation works: {e.Message}");
            }

            // Can't directly access private fields - compiler error!
            // account._balance = -500;  // CS0122: inaccessible due to
            // protection level

            // Read balance safely
            Console.WriteLine(
                $"\n[OK] Current balance: ${account.GetBalance():F2}"
            );

            // Get statement (protected copy)
            Console.WriteLine("\n[OK] Transaction History:");
            foreach (var statementLine in account.GetStatement())
            {
                Console.WriteLine($"   {statementLine}");
            }

            Console.WriteLine("\n[OK] BENEFITS:");
            Console.WriteLine(
                "- Business rules enforced (no negative balance)"
            );
            Console.WriteLine("- Internal state protected from corruption");
            Console.WriteLine("- Can change internal implementation freely");
            Console.WriteLine("- Clean, simple interface for clients");
            Console.WriteLine("- Automatic audit trail");
        }
    }
}