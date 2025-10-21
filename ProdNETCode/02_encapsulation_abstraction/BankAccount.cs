/*
BAD: No encapsulation - everything is exposed
*/
using System;
using System.Collections.Generic;

public class BankAccount
{
    /// <summary>
    /// Everything is public - no protection!
    /// </summary>
    public string Owner;
    public decimal Balance;  // Direct access allowed!
    public List<string> Transactions;  // Can be manipulated directly!

    public BankAccount(string owner, decimal balance)
    {
        Owner = owner;
        Balance = balance;
        Transactions = new List<string>();
    }

    public static void Main()
    {
        var account = new BankAccount("Alice", 1000);

        Console.WriteLine($"Initial balance: ${account.Balance}");

        // Problem 1: Can directly modify balance without validation
        account.Balance = -500;  // Negative balance?!
        Console.WriteLine($"[X] After direct manipulation: ${account.Balance}");

        // Problem 2: Can bypass business logic
        account.Balance += 1000000;  // Instant millionaire!
        Console.WriteLine($"[X] Became a millionaire: ${account.Balance}");

        // Problem 3: Can corrupt internal state
        account.Transactions = null;  // Null reference!
        Console.WriteLine($"[X] Corrupted transactions: {account.Transactions}");

        // Problem 4: If we change internal representation, all code breaks
        // e.g., if we want to store balance in cents instead of dollars

        Console.WriteLine("\n[X] PROBLEMS:");
        Console.WriteLine("- No validation or business rules enforced");
        Console.WriteLine("- Internal state can be corrupted");
        Console.WriteLine("- Can't change implementation without breaking clients");
        Console.WriteLine("- No audit trail or control");
    }
}