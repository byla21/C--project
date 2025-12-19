// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

class Program
{
    static string accountHolder;
    static int accountNumber;
    static double balance;
    static string createdDate;
    static bool accountExists = false;

    static string filePath = "account.txt";

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n- Applepie National Bank -");
            Console.WriteLine("1. Create an Account");
            Console.WriteLine("2. Open Existing Account");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Check Balance");
            Console.WriteLine("6. Display Account Details");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    OpenAccount();
                    break;
                case 3:
                    Deposit();
                    break;
                case 4:
                    Withdraw();
                    break;
                case 5:
                    CheckBalance();
                    break;
                case 6:
                    DisplayDetails();
                    break;
                case 7:
                    Console.WriteLine("Llosed.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 7);
    }

    static void CreateAccount()
    {
        if (accountExists)
        {
            Console.WriteLine("An account is already loaded.");
            return;
        }

        Console.Write("Account holder name: ");
        accountHolder = Console.ReadLine();

        Console.Write("Account number: ");
        accountNumber = Convert.ToInt32(Console.ReadLine());

        balance = 0;
        createdDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        accountExists = true;

        SaveAccount();
        Console.WriteLine("Account created and saved successfully.");
    }

    static void OpenAccount()
    {
        if (accountExists)
        {
            Console.WriteLine("An account is already loaded.");
            return;
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine("No account existed. Please create an account first.");
            return;
        }

        string[] data = File.ReadAllLines(filePath);

        accountHolder = data[0];
        accountNumber = int.Parse(data[1]);
        balance = double.Parse(data[2]);
        createdDate = data[3];
        accountExists = true;

        Console.WriteLine("Account opened successfully.");
    }

    static void Deposit()
    {
        if (!accountExists)
        {
            Console.WriteLine("No account detected. Please create or open an account first.");
            return;
        }

        Console.Write("Enter amount to deposit: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        balance += amount;
        SaveAccount();

        Console.WriteLine("Deposit successful.");
    }

    static void Withdraw()
    {
        if (!accountExists)
        {
            Console.WriteLine("No account detected. Please create or open an account first.");
            return;
        }

        Console.Write("Enter amount to withdraw: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        if (amount <= balance)
        {
            balance -= amount;
            SaveAccount();
            Console.WriteLine("Withdrawal successful.");
        }
        else
        {
            Console.WriteLine("Insufficient balance.");
        }
    }

    static void CheckBalance()
    {
        if (!accountExists)
        {
            Console.WriteLine("No account detected. Please create or open an account first.");
            return;
        }

        Console.WriteLine("Current Balance: " + balance);
    }

    static void DisplayDetails()
    {
        if (!accountExists)
        {
            Console.WriteLine("No account detected. Please create or open an account first.");
            return;
        }

        Console.WriteLine("\n| Account Details |");
        Console.WriteLine("Account Holder: " + accountHolder);
        Console.WriteLine("Account Number: " + accountNumber);
        Console.WriteLine("Account Created On: " + createdDate);
        Console.WriteLine("Balance: " + balance);
    }

    static void SaveAccount()
    {
        if (!accountExists)
            return;

        string[] data =
        {
            accountHolder,
            accountNumber.ToString(),
            balance.ToString(),
            createdDate
        };

        File.WriteAllLines(filePath, data);
    }
}

