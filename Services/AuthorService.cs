namespace myappdotnet.Service;

using Microsoft.EntityFrameworkCore;
using myappdotnet.Model;
public class AuthorService
{
    private readonly ApplicationDbContext context;

    public AuthorService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Author? CreateAuthor(string surname, string firs_name)
    {
        if (surname == null || firs_name == null)
        {
            return null;
        }
        var author = new Author();
        author.Surname = surname;
        author.FirstName = firs_name;
        context.Author.Add(author);
        context.SaveChanges();
        return author;
    }

    public Author? FindAuthorBySurnameAndFirstName(string surname, string firstName)
    {
        var author = context.Author
            .FirstOrDefault(a => a.Surname == surname && a.FirstName == firstName);

        return author;
    }
}
