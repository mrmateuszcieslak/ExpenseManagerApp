using System.Collections.Generic;
using ExpenseManagerApp.Models;

namespace ExpenseManagerApp.Repositories
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);
        void Delete(Expense expense);
        void Update(Expense expense);
        List<Expense> GetAll();
        decimal GetTotal();
       
    }
}

