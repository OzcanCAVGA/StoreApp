using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EfCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _repositoryContext
                .Books
                .Where(b => b.ID.Equals(id))
                .SingleOrDefault();

                if (book == null)
                    return NotFound(); //404

                return Ok(book);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest(); //400
                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _repositoryContext
                .Books
                .Where(b => b.ID.Equals(id))
                .SingleOrDefault();

                if (entity == null)
                    return NotFound();

                if (id != book.ID)
                    return BadRequest();

                entity.Title = book.Title;
                entity.Price = book.Price;
                _repositoryContext.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBooks([FromRoute(Name = "id")]int id)
        {
            try
            {
                var entity = _repositoryContext
                    .Books
                    .Where(b=>b.ID.Equals(id))
                    .SingleOrDefault();

                if(entity == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id:{id} could not found."
                    });
                }

                _repositoryContext.Books.Remove(entity);
                _repositoryContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}