using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Book1", Author = "Author1", PublishedDate = DateTime.Now.AddDays(-30) },
            new Book { Id = 2, Title = "Book2", Author = "Author2", PublishedDate = DateTime.Now.AddDays(-20) },
            new Book { Id = 3, Title = "Book3", Author = "Author3", PublishedDate = DateTime.Now.AddDays(-10) }
        };

        // GET: api/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Ok(_books);
        }

        // GET: api/book/1
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _books.Find(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/book
        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book newBook)
        {
            newBook.Id = GenerateNewId();
            _books.Add(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        // PUT: api/book/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book updatedBook)
        {
            var existingBook = _books.Find(b => b.Id == id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.PublishedDate = updatedBook.PublishedDate;

            return NoContent();
        }

        // DELETE: api/book/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookToRemove = _books.Find(b => b.Id == id);

            if (bookToRemove == null)
            {
                return NotFound();
            }

            _books.Remove(bookToRemove);

            return NoContent();
        }

        private int GenerateNewId()
        {
            // Just a simple method to generate a new unique ID for a book
            return _books.Count + 1;
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}