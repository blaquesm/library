namespace Backend.Models;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime? ReservedAt { get; set; } // Когда была сделана бронь
    public DateTime? IssuedAt { get; set; } // Когда книга была выдана (может быть null, если не выдана)
    public DateTime? ReturnedAt { get; set; } // Когда книга была возвращена (может быть null, если не возвращена)
}