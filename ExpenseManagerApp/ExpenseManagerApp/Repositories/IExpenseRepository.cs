using System.Collections.Generic;
using ExpenseManagerApp.Models;

namespace ExpenseManagerApp.Repositories
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);
        List<Expense> GetAll();
        decimal GetTotal();
    }
}

