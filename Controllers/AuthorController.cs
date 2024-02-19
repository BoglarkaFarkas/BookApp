
using Microsoft.AspNetCore.Mvc;
using myappdotnet.DTOs;
using myappdotnet.Model;
using myappdotnet.Service;

namespace myappdotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly AuthorService authorService;
    public AuthorController(ApplicationDbContext context)
    {
        this.context = context;
        this.authorService = new AuthorService(context);
    }



    [HttpPost]
    [Route("create-author")]
    public IActionResult AuthorCreate([FromBody] AuthorDTO authorDTO)
    {
        try
        {
            if (authorDTO == null || authorDTO.Surname == null || authorDTO.First_name == null)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            var createAuthor = authorService.CreateAuthor(authorDTO.Surname, authorDTO.First_name);
            if (createAuthor == null)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            else
            {
                return Ok(new { message = "Author was created" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }

}
