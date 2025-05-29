using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    [Route("odata/Presses")]
    public class PressesController : ODataController
    {
        private readonly BookStoreContext _db;

        public PressesController(BookStoreContext context)
        {
            _db = context;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // Khởi tạo dữ liệu mẫu nếu database trống
            if (!_db.Presses.Any())
            {
                foreach (var book in DataSource.GetBooks())
                {
                    if (!_db.Presses.Any(p => p.Id == book.Press.Id))
                    {
                        _db.Presses.Add(book.Press);
                    }
                }
                _db.SaveChanges();
            }
        }

        [EnableQuery]
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(_db.Presses);
        }
    }
}
