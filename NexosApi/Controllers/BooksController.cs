using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexosApi.Data;
using NexosApi.Models;
using NexosApi.Models.Dto;
using NexosApi.Repository;

namespace NexosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        protected ResponseDto _response;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _response = new ResponseDto();
            _response.IsOk = true;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookRepository.GetABooks();
                _response.Result = books;
                _response.ResultMessage = "Lista de libros.";
            }
            catch (Exception ex)
            {
                _response.IsOk = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                _response.IsOk = false;
                _response.ResultMessage = "Libro no existe.";
                return NotFound(_response);
            }

            _response.Result = book;
            _response.ResultMessage = "Informacion  del Libro.";
            return Ok(_response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            try
            {
                BookDto _bookDto = await _bookRepository.CreateUpdate(bookDto);
                _response.Result = _bookDto;
                _response.ResultMessage = "Se Actualizo Correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsOk = false;
                _response.ResultMessage = "Error actualizando.";
                _response.Errors = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            try
            {

                var authorDto = await _bookRepository.GetAuthor(bookDto.AuthorId);
                var publishetDto = await _bookRepository.GetPublisher(bookDto.PublisherId);
                int countPublisherBook = await _bookRepository.GetBookByPublisher(bookDto.PublisherId);

                if (authorDto==null)
                {
                    _response.IsOk = false;
                    _response.ResultMessage = "El autor no esta registrado.";
                    return BadRequest(_response);
                }

                if (publishetDto == null)
                {
                    _response.IsOk = false;
                    _response.ResultMessage = "La editorial no esta registrada.";
                    return BadRequest(_response);
                }
                else if (countPublisherBook>publishetDto.MaxBook)
                {
                    _response.IsOk = false;
                    _response.ResultMessage = "No es posible registrar el libro, se alcanzó el máximo permitido.";
                    return BadRequest(_response);
                }


                BookDto _bookDto = await _bookRepository.CreateUpdate(bookDto);
                _response.Result = _bookDto;
                _response.ResultMessage = "Se Actualizo Correctamente.";
                return CreatedAtAction("GetBook", new { id = _bookDto.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.IsOk = false;
                _response.ResultMessage = "Error actualizando.";
                _response.Errors = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                bool Deleted = await _bookRepository.DeleteBook(id);
                if (Deleted)
                {
                    _response.Result = Deleted;
                    _response.ResultMessage = "Se elimino correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsOk = Deleted;
                    _response.ResultMessage = "Error eliminando.";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsOk = false;
                _response.Errors = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

    }
}
