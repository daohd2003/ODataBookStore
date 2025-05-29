using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    [Route("odata/Books")]
    public class BooksController : ODataController
    {
        private readonly BookStoreContext _db;

        public BooksController(BookStoreContext context)
        {
            _db = context;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (!_db.Books.Any())
            {
                foreach (var b in DataSource.GetBooks())
                {
                    _db.Books.Add(b);
                    _db.Presses.Add(b.Press);
                }
                _db.SaveChanges();
            }
        }

        // GET odata/Books
        [EnableQuery(PageSize = 1)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Books);
        }

        // GET odata/Books(1)
        [EnableQuery]
        [HttpGet]
        public IActionResult Get(int key)
        {
            var book = _db.Books.FirstOrDefault(c => c.Id == key);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST odata/Books
        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Created(book);
        }

        // DELETE odata/Books(1)
        [EnableQuery]
        [HttpDelete]
        public IActionResult Delete([FromBody] int key)
        {
            var book = _db.Books.FirstOrDefault(c => c.Id == key);
            if (book == null) return NotFound();

            _db.Books.Remove(book);
            _db.SaveChanges();
            return NoContent();
        }
    }
}