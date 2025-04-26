using System;
using System.Globalization;
using System.Linq;
using ExpenseManagerApp.Models;
using ExpenseManagerApp.Repositories;

namespace ExpenseManagerApp.Services
{
    public class ExpenseManager
    {
        private readonly IExpenseRepository repository;

        public ExpenseManager(IExpenseRepository repository)
        {
            this.repository = repository;
        }

        public void AddExpense()
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter amount: ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            var expense = new Expense(description, amount, category);
            repository.Add(expense);

            Console.WriteLine("Expense added successfully.");
        }

        public void ViewExpenses()
        {
            var expenses = repository.GetAll();
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses recorded.");
                return;
            }

            Console.WriteLine("\n--- Expenses ---");
            foreach (var expense in expenses)
            {
                expense.Display();
            }
        }

        public void ShowTotal()
        {
            decimal total = repository.GetTotal();
            Console.WriteLine($"\nTotal Expenses: {total:C}");
        }

        public void GenerateMonthlyReport()
        {
            Console.Write("Enter month (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month.");
                return;
            }

            var expenses = repository.GetAll()
                                      .Where(e => e.Date.Month == month)
                                      .ToList();

            if (expenses.Count == 0)
            {
                Console.WriteLine($"No expenses found for month {month}.");
                return;
            }

            var englishCulture = new CultureInfo("en-US");

            Console.WriteLine($"\n--- Report for {englishCulture.DateTimeFormat.GetMonthName(month)} ---");
            foreach (var expense in expenses)
            {
                expense.Display();
            }

            decimal monthlyTotal = expenses.Sum(e => e.Amount);
            Console.WriteLine($"\nTotal for {englishCulture.DateTimeFormat.GetMonthName(month)}: {monthlyTotal:C}");
        }
    }
}
