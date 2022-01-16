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
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        protected ResponseDto _response;
        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
            _response = new ResponseDto();
            _response.IsOk = true;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            try
            {
                var authors = await _publisherRepository.GetAPublishers();
                _response.Result = authors;
                _response.ResultMessage = "Lista de Editoriales.";
            }
            catch (Exception ex)
            {
                _response.IsOk = false;
                _response.Errors = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Publisher))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var author = await _publisherRepository.GetPublisher(id);

            if (author == null)
            {
                _response.IsOk = false;
                _response.ResultMessage = "Editorial no existe.";
                return NotFound(_response);
            }

            _response.Result = author;
            _response.ResultMessage = "Informacion  de la Editorial.";
            return Ok(_response);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherDto publisherDto)
        {
            try
            {
                PublisherDto _publisherDto = await _publisherRepository.CreateUpdate(publisherDto);
                _response.Result = _publisherDto;
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
        public async Task<ActionResult<Publisher>> CreatePublisher(PublisherDto publisherDto)
        {
            try
            {
                PublisherDto _publisherDto = await _publisherRepository.CreateUpdate(publisherDto);
                _response.Result = _publisherDto;
                _response.ResultMessage = "Se Actualizo Correctamente.";
                return CreatedAtAction("GetPublisher", new { id = _publisherDto.Id}, _response);

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
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                bool Deleted = await _publisherRepository.DeletePublisher(id);
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

        //private bool PublisherExists(int id)
        //{
        //    return _context.Publishers.Any(e => e.Id == id);
        //}
    }
}
