using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books);
    }

    [HttpPost("add")]
    [Authorize(Roles = "Librarian")]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpPost("delete")]
    [Authorize(Roles = "Librarian")]
    public async Task<IActionResult> DeleteBook([FromBody] Book book)
    {
        var existingBook = await _context.Books.FindAsync(book.Id);
        if (existingBook == null)
        {
            return NotFound("Книга не найдена.");
        }

        _context.Books.Remove(existingBook);
        await _context.SaveChangesAsync();

        return Ok($"Книга с ID {book.Id} успешно удалена.");
    }
}