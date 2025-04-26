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

        public void DeleteExpense()
        {
            var expenses = repository.GetAll();
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses to delete.");
                return;
            }

            Console.WriteLine("Select the ID of the expense to delete:");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"ID: {expense.Id}, Description: {expense.Description}, Amount: {expense.Amount:C}");
            }

            Console.Write("Enter ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var expenseToDelete = expenses.FirstOrDefault(e => e.Id == id);
            if (expenseToDelete == null)
            {
                Console.WriteLine("Expense not found.");
                return;
            }

            repository.Delete(expenseToDelete);
            Console.WriteLine("Expense deleted successfully.");
        }

        public void EditExpense()
        {
            var expenses = repository.GetAll();
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses to edit.");
                return;
            }

            Console.WriteLine("Select the ID of the expense to edit:");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"ID: {expense.Id}, Description: {expense.Description}, Amount: {expense.Amount:C}");
            }

            Console.Write("Enter ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var expenseToEdit = expenses.FirstOrDefault(e => e.Id == id);
            if (expenseToEdit == null)
            {
                Console.WriteLine("Expense not found.");
                return;
            }

            Console.Write("Enter new description (leave empty to keep current): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
                expenseToEdit.Description = newDescription;

            Console.Write("Enter new amount (leave empty to keep current): ");
            string amountInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(amountInput))
            {
                if (decimal.TryParse(amountInput, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal newAmount))
                    expenseToEdit.Amount = newAmount;
                else
                    Console.WriteLine("Invalid amount. Keeping previous amount.");
            }

            Console.Write("Enter new category (leave empty to keep current): ");
            string newCategory = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCategory))
                expenseToEdit.Category = newCategory;

            repository.Update(expenseToEdit);
            Console.WriteLine("Expense updated successfully.");
        }
    }
}
