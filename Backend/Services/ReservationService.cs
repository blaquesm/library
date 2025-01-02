using Backend.Interface;
using Backend.Models;

namespace Backend.Services;

public class ReservationService : IReservationService
{
    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public bool CanBeReserved(int bookId)
    {
        var book = _bookRepository.GetById(bookId);
        return book != null && !book.IsReserved && !book.IsIssued;
    }

    public bool CanBeIssued(int bookId)
    {
        var book = _bookRepository.GetById(bookId);
        return book != null && book.IsReserved && !book.IsIssued;
    }
}