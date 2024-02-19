
using Microsoft.AspNetCore.Mvc;
using myappdotnet.DTOs;
using myappdotnet.Model;
using myappdotnet.Service;

namespace myappdotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly BookService bookService;
    public BookController(ApplicationDbContext context)
    {
        this.context = context;
        this.bookService = new BookService(context);
    }



    [HttpGet]
    [Route("book-id/{id}")]
    public IActionResult GetLocationById(int id)
    {
        try
        {
            var book = bookService.FindBookById(id);
            if (book == null)
            {
                return NotFound(new ErrorResponseDTO { Status = 404, Error = "Book do not exist" });
            }
            ICollection<Author> authors = bookService.FindAuthorsForBook(id);

            var authorDTOs = authors.Select(author => new AuthorDTO
            {
                Surname = author.Surname,
                First_name = author.FirstName
            }).ToList();
            var bookDTO = new BookDTO
            {
                Title = book.Title,
                Authors = authorDTOs,
                Publication_year = book.PublicationYear
            };

            return Ok(bookDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }

    [HttpPost]
    [Route("create-book")]
    public IActionResult BookCreate([FromBody] BookDTO bookDTO)
    {
        try
        {
            if (bookDTO == null || bookDTO.Title == null || bookDTO.Authors == null)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            var createBook = bookService.CreateBook(bookDTO.Authors, bookDTO.Title, bookDTO.Publication_year);
            if (createBook == null)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            else
            {
                return Ok(new { message = "Book was created" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }

    [HttpPut]
    [Route("change-book/{id}")]
    public IActionResult ChangeBook([FromBody] BookDTO bookDTO, int id)
    {
        try
        {
            if (bookDTO == null)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            var book = bookService.FindBookById(id);
            if (book == null)
            {
                return NotFound(new ErrorResponseDTO { Status = 404, Error = "Book do not exist" });
            }
            bool isChanged = bookService.UpdateBook(bookDTO.Authors, bookDTO.Title, bookDTO.Publication_year, id);
            if (isChanged == false)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            else
            {
                return Ok(new { message = "Book was updated" });
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }
    [HttpDelete]
    [Route("delete-book/{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            bool deletedBook = bookService.RemoveBook(id);
            if (deletedBook == false)
            {
                return NotFound(new ErrorResponseDTO { Status = 404, Error = "Book do not exist" });
            }else{
                return Ok(new { message = "Book was deleted" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }
}
