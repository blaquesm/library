using Backend.Interface;
using Backend.Models;

namespace Backend.Data;

public class BookService : IRepository<Book>
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Book GetById(int id)
    {
        return _context.Books.FirstOrDefault(b => b.Id == id);
    }

    public IQueryable<Book> GetAll()
    {
        return _context.Books.AsQueryable();
    }

    public void Add(Book entity)
    {
        _context.Books.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Book entity)
    {
        _context.Books.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Book entity)
    {
        _context.Books.Remove(entity);
        _context.SaveChanges();
    }

}