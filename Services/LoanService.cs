namespace myappdotnet.Service;

using Microsoft.EntityFrameworkCore;
using myappdotnet.Model;
public class LoanService
{
    private readonly ApplicationDbContext context;

    public LoanService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public bool CreateLoan(int bookId, string email)
    {
        if (email == null || !context.Book.Any(b => b.Id == bookId))
        {
            return false;
        }

        var existingLoan = context.Loan.FirstOrDefault(l => l.BookId == bookId && l.IsFree == false);

        if (existingLoan != null)
        {
            return false;
        }
        var loan = new Loan();
        loan.BookId = bookId;
        loan.Email = email;
        context.Loan.Add(loan);
        context.SaveChanges();
        return true;
    }

    public string? ReturnBook(int id)
    {
        var existingLoan = context.Loan.FirstOrDefault(l => l.Id == id && l.IsFree == false);

        if (existingLoan == null)
        {
            return null;
        }
        existingLoan.IsFree = true;
        context.SaveChanges();
        return existingLoan.Email;
    }
}
