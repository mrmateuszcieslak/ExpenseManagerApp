using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ExpenseManagerApp.Models;

namespace ExpenseManagerApp.Repositories
{
    public class JsonExpenseRepository : IExpenseRepository
    {
        private readonly string filePath = "expenses.json";
        private List<Expense> expenses = new List<Expense>();

        public JsonExpenseRepository()
        {
            LoadExpenses();
        }

        public void Add(Expense expense)
        {
            expenses.Add(expense);
            SaveExpenses();
        }

        public List<Expense> GetAll()
        {
            return new List<Expense>(expenses);
        }

        public decimal GetTotal()
        {
            return expenses.Sum(e => e.Amount);
        }

        public void Delete(Expense expense)
        {
            var existingExpense = expenses.FirstOrDefault(e => e.Id == expense.Id);
            if (existingExpense != null)
            {
                expenses.Remove(existingExpense);
                SaveExpenses();
            }
        }

        public void Update(Expense expense)
        {
            var existingExpense = expenses.FirstOrDefault(e => e.Id == expense.Id);
            if (existingExpense != null)
            {
                existingExpense.Description = expense.Description;
                existingExpense.Amount = expense.Amount;
                existingExpense.Category = expense.Category;
                existingExpense.Date = expense.Date; // Jeśli chcesz też aktualizować datę

                SaveExpenses();
            }
        }

        private void LoadExpenses()
        {
            if (!File.Exists(filePath))
                return;

            var json = File.ReadAllText(filePath);
            List<Expense>? loadedExpenses = JsonSerializer.Deserialize<List<Expense>>(json);

            if (loadedExpenses != null)
            {
                expenses = loadedExpenses;
            }
        }

        private void SaveExpenses()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(expenses, options);
            File.WriteAllText(filePath, json);
        }
    }
}
