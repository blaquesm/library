using Backend.Interface;
using Backend.Models;

namespace Backend.Data;

public class ReservationRepository : IRepository<Reservation>
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Reservation GetById(int id)
    {
        return _context.Reservations.FirstOrDefault(r => r.Id == id);
    }

    public IQueryable<Reservation> GetAll()
    {
        return _context.Reservations.AsQueryable();
    }

    public void Add(Reservation entity)
    {
        _context.Reservations.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Reservation entity)
    {
        _context.Reservations.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Reservation entity)
    {
        _context.Reservations.Remove(entity);
        _context.SaveChanges();
    }
}