using System;

namespace ExpenseManagerApp.Models
{
    public class Expense
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public Expense(string description, decimal amount, string category)
        {
            Description = description;
            Amount = amount;
            Category = category;
            Date = DateTime.Now;
        }

        public Expense(string description, decimal amount, string category, DateTime date)
        {
            Description = description;
            Amount = amount;
            Category = category;
            Date = date;
        }

        public void Display()
        {
            Console.WriteLine($"{Date:yyyy-MM-dd} | {Category} | {Description} | {Amount:C}");
        }
    }
}
