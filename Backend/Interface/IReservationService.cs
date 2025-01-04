namespace Backend.Interface;

public interface IReservationService
{
    bool CanBeReserved(int bookId);
    bool CanBeIssued(int bookId);
    void ReserveBook(int bookId, int userId);
    void UnreserveBook (int bookId, int userId);
    void IssueBook(int bookId, int userId);
    bool ReturnBook(int bookId);
}