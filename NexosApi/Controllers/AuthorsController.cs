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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        protected ResponseDto _response;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _response = new ResponseDto();
            _response.IsOk = true;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAuthors();
                _response.Result = authors;
                _response.ResultMessage = "Lista de autores.";
            }
            catch (Exception ex)
            {
                _response.IsOk = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}",Name = "GetAuthor")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthor(id);

            if (author == null)
            {
                _response.IsOk = false;
                _response.ResultMessage = "Autor no existe.";
                return NotFound(_response);
            }

            _response.Result = author;
            _response.ResultMessage = "Informacion  del Autor.";
            return Ok(_response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDto authorDto)
        {
            try
            {
                AuthorDto _authorDto = await _authorRepository.CreateUpdate(authorDto);
                _response.Result = _authorDto;
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
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            try
            {
                AuthorDto _authorDto = await _authorRepository.CreateUpdate(authorDto);
                _response.Result = _authorDto;
                _response.ResultMessage = "Se Actualizo Correctamente.";
                return CreatedAtAction("GetAuthor", new { id = _authorDto.Id }, _response);

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
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                bool Deleted = await _authorRepository.DeleteAuthor(id);
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
