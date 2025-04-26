using System;

namespace ExpenseManagerApp.Models
{
    public class Expense
    {
        private static int nextId = 1; 

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public Expense(string description, decimal amount, string category)
        {
            Id = nextId++; 
            Description = description;
            Amount = amount;
            Category = category;
            Date = DateTime.Now;
        }

        public void Display()
        {
            Console.WriteLine($"ID: {Id}, Description: {Description}, Amount: {Amount:C}, Category: {Category}, Date: {Date:d}");
        }
    }
}
