using System;
using ExpenseManagerApp.Repositories;
using ExpenseManagerApp.Services;

namespace ExpenseManagerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowLoading();

            IExpenseRepository repository = new JsonExpenseRepository();
            ExpenseManager manager = new ExpenseManager(repository);

            bool running = true;

            while (running)
            {
                ShowMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.AddExpense();
                        break;
                    case "2":
                        manager.ViewExpenses();
                        break;
                    case "3":
                        manager.ShowTotal();
                        break;
                    case "4":
                        manager.GenerateMonthlyReport();
                        break;
                    case "5":
                        manager.DeleteExpense();
                        break;
                    case "6":
                        manager.EditExpense();
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("\nThank you for using Expense Manager!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowLoading()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Starting Expense Manager...\n");

            int totalBlocks = 30;
            Console.ForegroundColor = ConsoleColor.Green;

            for (int percent = 0; percent <= 100; percent++)
            {
                int filledBlocks = percent * totalBlocks / 100;
                string progressBar = "[" + new string('#', filledBlocks) + new string('-', totalBlocks - filledBlocks) + $"] {percent}%";

                Console.Write($"\r{progressBar}");
                System.Threading.Thread.Sleep(30);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Loading complete!");
            System.Threading.Thread.Sleep(1000);
            Console.ResetColor();
            Console.Clear();
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("         EXPENSE MANAGER          ");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Show Total Expenses");
            Console.WriteLine("4. Monthly Report");
            Console.WriteLine("5. Delete Expense");
            Console.WriteLine("6. Edit Expense");
            Console.WriteLine("7. Exit");
            Console.WriteLine("==================================");
            Console.Write("Choose an option (1-7): ");
        }
    }
}
