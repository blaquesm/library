using Backend.Interface;
using Backend.Models;

namespace Backend.Services;

public class ReservationService : IReservationService
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Reservation> _reservationRepository;

    public ReservationService(IRepository<Book> bookRepository, IRepository<Reservation> reservationRepository)
    {
        _bookRepository = bookRepository;
        _reservationRepository = reservationRepository;
    }

    public bool CanBeReserved(int bookId)
    {
        var book = _bookRepository.GetById(bookId);
        return book != null && !book.IsReserved && !book.IsIssued && book.IsReturnBook;
    }

    public bool CanBeIssued(int bookId)
    {
        var book = _bookRepository.GetById(bookId);
        return book != null && book.IsReserved && !book.IsIssued && book.IsReturnBook;
    }

     public void ReserveBook(int bookId, int userId)
    {
        var book = _bookRepository.GetById(bookId);
        if (!CanBeReserved(bookId))
        {
            throw new InvalidOperationException("Книга не доступна для бронирования.");
        }

        book.IsReserved = true;
        _bookRepository.Update(book);

        var reservation = new Reservation
        {
            BookId = bookId,
            UserId = userId,
            ReservedAt = DateTime.UtcNow
        };

        _reservationRepository.Add(reservation);
    }

    public void UnreserveBook(int bookId, int userId)
    {
        var reservation = _reservationRepository.GetAll()
            .FirstOrDefault(r => r.BookId == bookId && r.UserId == userId);

        if (reservation == null)
        {
            throw new InvalidOperationException("Бронирование не найдено или не принадлежит указанному пользователю.");
        }

        _reservationRepository.Delete(reservation);

        var book = _bookRepository.GetById(bookId);
        if (book != null)
        {
            book.IsReserved = false;
            _bookRepository.Update(book);
        }

        _reservationRepository.Update(reservation);
    }

    public void IssueBook(int bookId, int userId)
    {
        var book = _bookRepository.GetById(bookId);
        if (!CanBeIssued(bookId))
        {
            throw new InvalidOperationException("Книга не доступна для выдачи.");
        }

        book.IsIssued = true;
        book.IsReserved = false;
        _bookRepository.Update(book);

        var reservation = _reservationRepository.GetAll()
            .FirstOrDefault(r => r.BookId == bookId && r.UserId == userId && r.ReturnedAt == null);

        if (reservation == null)
        {
            throw new InvalidOperationException("Резерв не найден для выдачи.");
        }

        reservation.IssuedAt = DateTime.UtcNow;
        _reservationRepository.Update(reservation);
    }

    public bool  ReturnBook(int bookId)
    {
        var book = _bookRepository.GetById(bookId);
        if (book != null && book.IsIssued)
        {
            book.IsIssued = false;
            book.IsReserved = false;
            _bookRepository.Update(book);
            return true;
        }
        return false;
    }
}