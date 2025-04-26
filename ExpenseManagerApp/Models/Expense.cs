using System;

namespace ExpenseManagerApp.Models
{
    public class Expense
    {
        private static int nextId = 1;
        private decimal amount;
        private string description;
        private string category;
        private DateTime date;

        public int Id { get; set; }

        public string Description
        {
            get => description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be empty.");
                }
                description = value;
            }
        }

        public decimal Amount
        {
            get => amount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Amount must be greater than 0.");
                }
                amount = value;
            }
        }

        public string Category
        {
            get => category;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Category cannot be empty.");
                }
                category = value;
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                if (value.Date < DateTime.Today)
                {
                    throw new ArgumentException("Date cannot be in the past.");
                }
                date = value;
            }
        }

        public Expense(string description, decimal amount, string category)
        {
            Id = nextId++;
            Description = description;
            Amount = amount;
            Category = category;
            Date = DateTime.Today;
        }

        public void Display()
        {
            Console.WriteLine($"ID: {Id}, Description: {Description}, Amount: {Amount:C}, Category: {Category}, Date: {Date:d}");
        }
    }
}
