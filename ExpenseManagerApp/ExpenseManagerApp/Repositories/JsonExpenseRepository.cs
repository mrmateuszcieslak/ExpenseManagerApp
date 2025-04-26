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

        private void LoadExpenses()
        {
            if (!File.Exists(filePath))
                return;

            var json = File.ReadAllText(filePath);
            var loadedExpenses = JsonSerializer.Deserialize<List<Expense>>(json);

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

