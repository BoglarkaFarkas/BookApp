
using Microsoft.AspNetCore.Mvc;
using myappdotnet.DTOs;
using myappdotnet.Model;
using myappdotnet.Service;

namespace myappdotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class LoanController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly LoanService loanService;
    public LoanController(ApplicationDbContext context)
    {
        this.context = context;
        this.loanService = new LoanService(context);
    }



    [HttpPost]
    [Route("create-loan")]
    public IActionResult LoanCreate([FromBody] LoanDTO loanDTO)
    {
        try
        {
            if (loanDTO == null || loanDTO.Email == null)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            bool isCreated = loanService.CreateLoan(loanDTO.BookId, loanDTO.Email);
            if (isCreated == false)
            {
                return BadRequest(new ErrorResponseDTO { Status = 400, Error = "Invalid data" });
            }
            else
            {
                return Ok(new { message = "Loan was created" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }

    [HttpPut]
    [Route("return-loan/{id}")]
    public IActionResult ReturnedLoan(int id)
    {
        try
        {
            var email = loanService.ReturnBook(id);
            if (email == null)
            {
                return NotFound(new ErrorResponseDTO { Status = 404, Error = "Book does not exist or Book is free" });
            }
            return Ok(new { message = $"Dear {email}, thank you for returning the borrowed book, we hope you enjoyed it." });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { error = "Internal Server Error" });
        }

    }

}
