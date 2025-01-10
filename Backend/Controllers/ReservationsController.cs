using Backend.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _bookService;

    public ReservationsController(IReservationService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost("{id}/reserve")]
    [Authorize(Roles = "Client")]
    public IActionResult ReserveBook(int id, [FromBody] int userId)
    {
        try
        {
            _bookService.ReserveBook(id, userId);
            return Ok("Книга успешно забронирована.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/unreserve")]
    [Authorize(Roles = "Client")]
    public IActionResult UnreserveBook(int id, [FromBody] int userId)
    {
        try
        {
            _bookService.UnreserveBook(id, userId);
            return Ok("Бронирование книги успешно отменено.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/issue")]
    [Authorize(Roles = "Librarian")]
    public IActionResult IssueBook(int id, [FromBody] int userId)
    {
        try
        {
            _bookService.IssueBook(id, userId);
            return Ok("Книга успешно выдана.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/return")]
    [Authorize(Roles = "Librarian")]
    public IActionResult ReturnBook(int id)
    {
        try
        {
            _bookService.ReturnBook(id);
            return Ok("Книга успешно возвращена.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}