namespace myappdotnet.Service;
using myappdotnet.DTOs;
using myappdotnet.Model;
public class BookService
{
    private readonly ApplicationDbContext context;
    private readonly AuthorService authorService;

    public BookService(ApplicationDbContext context)
    {
        this.context = context;
        this.authorService = new AuthorService(context);
    }

    public Book? FindBookById(int id)
    {
        var book = context.Book.FirstOrDefault(b => b.Id == id);
        return book;
    }
    public ICollection<Author> FindAuthorsForBook(int id)
    {
        ICollection<Author> authors = new List<Author>();
        var bookAuthors = context.BookAuthor.Where(ba => ba.BookId == id).ToList();
        foreach (var bookAuthor in bookAuthors)
        {
            var author = context.Author.FirstOrDefault(a => a.Id == bookAuthor.AuthorId);
            if (author != null)
            {
                authors.Add(author);
            }
        }
        return authors;
    }

    public Book? CreateBook(ICollection<AuthorDTO> authors, string title, int publicationYear)
    {
        if (authors == null || title == null)
        {
            return null;
        }
        var book = new Book();
        book.Title = title;
        book.PublicationYear = publicationYear;
        context.Book.Add(book);
        context.SaveChanges();
        foreach (var author in authors)
        {
            var authBook = new BookAuthor();
            if (author.Surname != null && author.First_name != null)
            {
                var a = authorService.FindAuthorBySurnameAndFirstName(author.Surname, author.First_name);
                if (a == null)
                {
                    a = authorService.CreateAuthor(author.Surname, author.First_name);
                }
                authBook.AuthorId = a.Id;
                authBook.BookId = book.Id;
                context.BookAuthor.Add(authBook);
            }
        }

        context.SaveChanges();

        return book;
    }

    public bool UpdateBook(ICollection<AuthorDTO> authors, string title, int publicationYer, int id)
    {
        var book = context.Book.Find(id);
        if (book == null)
        {
            return false;
        }

        if (title != null)
        {
            book.Title = title;
        }
        if (publicationYer > 0)
        {
            book.PublicationYear = publicationYer;
        }

        if (authors != null)
        {
            var existingAuthors = context.BookAuthor.Where(ba => ba.BookId == id).Select(ba => ba.Author).ToList();

            if (!AuthorsAreEqual(authors, existingAuthors))
            {
                context.BookAuthor.RemoveRange(context.BookAuthor.Where(ba => ba.BookId == id));

                foreach (var author in authors)
                {
                    if (author.Surname != null && author.First_name != null)
                    {
                        var existingAuthor = authorService.FindAuthorBySurnameAndFirstName(author.Surname, author.First_name);

                        if (existingAuthor == null)
                        {
                            existingAuthor = authorService.CreateAuthor(author.Surname, author.First_name);
                        }

                        context.BookAuthor.Add(new BookAuthor { AuthorId = existingAuthor.Id, BookId = id });
                    }
                }
            }
        }

        context.SaveChanges();

        return true;
    }

    public bool RemoveBook(int id)
    {
        var book = FindBookById(id);
        if (book == null)
        {
            return false;
        }
        var relatedBookAuthors = context.BookAuthor.Where(ba => ba.BookId == id);
        context.BookAuthor.RemoveRange(relatedBookAuthors);
        context.Book.Remove(book);
        context.SaveChanges();
        return true;
    }

    private bool AuthorsAreEqual(ICollection<AuthorDTO> newAuthors, ICollection<Author> existingAuthors)
    {
        if (newAuthors.Count != existingAuthors.Count)
        {
            return false;
        }

        foreach (var author in existingAuthors)
        {
            if (!newAuthors.Any(a => a.Surname == author.Surname && a.First_name == author.FirstName))
            {
                return false;
            }
        }

        return true;
    }

}
