using System;
using Text.Link;
using System.Text;
using System.Connections
using System.Console
using System.data.sqlclient
using System.Threading.Thread
using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Copyright (C)devsimsek.
 * Simple ATM Software
 * Created for learning purposes.
 * Total Time: 37 minutes
 */
namespace tests
{
    class Program
    {
        class Records
        {
            public string AccountId;
            public string User;
            public decimal Balance;
        }

        private static List<Records> records = new List<Records>();

        /**
         * Main Function
         */
        private static void Main()
        {
            Console.WriteLine("Welcome to devsimsek atm example built on c#");
            Console.WriteLine("Options;");
            Console.WriteLine("\t1. Create Account On c# Bank");
            Console.WriteLine("\t2. Log In Your Existing Account");
            Console.WriteLine("\t3. Shutdown System");
            Console.Write("Select Operation> ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreateAccount(records);
                    break;
                case "2":
                    AccountController(records);
                    break;
                case "3":
                    Console.WriteLine("All Data Has Been Erased. Goodbye.");
                    break;
            }
        }

        /**
         * CreateAccount
         * Creates account on selected records list
         */
        private static void CreateAccount(List<Records> records)
        {
            var random = new Random();
            Console.Write("Name And Surname> ");
            var user = Console.ReadLine();

            var accountId = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            records.Add(new Records() {AccountId = accountId, User = user, Balance = 0.0m});
            Console.WriteLine("Account Created. Details;");
            Console.WriteLine($"\tAccountId: {accountId}");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            // Clear The Console And Call Main Again.
            Console.Clear();
            Main();
        }

        private static void AccountController(List<Records> records)
        {
            var systemShutdown = false;
            while (!systemShutdown)
            {
                Console.Write("Enter Bank AccountId: ");
                var input = Console.ReadLine();
                foreach (var account in records)
                {
                    if (input != null && input.Equals(account.AccountId))
                    {
                        Console.WriteLine($"Welcome {account.User}. Your Current Balance: {account.Balance}");
                        var atmModeShouldClose = false;
                        while (!atmModeShouldClose)
                        {
                            Console.WriteLine("Options;");
                            Console.WriteLine("\t1. View Balance");
                            Console.WriteLine("\t2. Deposit Money");
                            Console.WriteLine("\t3. Withdraw Money");
                            Console.WriteLine("\t4. Change Account");
                            Console.WriteLine("\t5. Quit");
                            Console.Write("Enter Operation> ");
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine($"Balance: {account.Balance}");
                                    break;
                                case "2":
                                    Console.Write("Enter Quantity> ");
                                    input = Console.ReadLine();
                                    Console.Clear();
                                    Console.WriteLine($"Depositing {input} To Account...");
                                    if (decimal.TryParse(input, out var deposit))
                                    {
                                        account.Balance += deposit;
                                        Console.WriteLine($"New Balance: {account.Balance}");
                                        break;
                                    }

                                    Console.Clear();
                                    Console.WriteLine("Error: Please Specify Quantity For Deposit...");
                                    break;
                                case "3":
                                    Console.Write("Enter Quantity> ");
                                    input = Console.ReadLine();
                                    if (decimal.TryParse(input, out var withdraw))
                                    {
                                        if (withdraw <= account.Balance)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Withdrawing {input} From Account...");
                                            account.Balance -= withdraw;
                                            Console.WriteLine($"New Balance: {account.Balance}");
                                            break;
                                        }

                                        Console.Clear();
                                        Console.WriteLine("Error: insufficient funds.");
                                        break;
                                    }

                                    Console.Clear();
                                    Console.WriteLine("Error: Please Specify Quantity For Withdraw...");
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("Logging out...");
                                    atmModeShouldClose = true;
                                    break;
                                case "5":
                                    atmModeShouldClose = true;
                                    systemShutdown = true;
                                    Console.WriteLine("All Data Has Been Erased. Goodbye.");
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
