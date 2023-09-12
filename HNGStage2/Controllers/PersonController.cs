using HNGStage2.Models;
using HNGStage2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HNGStage2.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetPerson(string user_id) 
        {
            ApiResponse response = new();

            if (user_id == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "user_id cannot be null";
                return BadRequest(response);
            }

            var person = await _personRepository.GetPerson(user_id);

            if (person == null)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = "person not found";
                return NotFound(response);
            }

            return Ok(person);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonDto addPerson)
        {
            ApiResponse response = new();
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await _personRepository.AddPerson(addPerson);
            if (result > 0)
            {
                response.StatusCode = StatusCodes.Status201Created;
                response.Message = "Successfully added person";
                return StatusCode(StatusCodes.Status200OK, response);
            }
         
            return StatusCode(StatusCodes.Status500InternalServerError, "failed to add person");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{user_id}")]
        public async Task<IActionResult> UpdatePerson(string user_id, [FromBody] PersonDto updatePerson)
        {
            ApiResponse response = new();
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var person = await _personRepository.GetPerson(user_id);
            if (person == null)
            {
                response.StatusCode =StatusCodes.Status404NotFound;
                response.Message = "user not found";
                return StatusCode(StatusCodes.Status404NotFound, response);
            }

            person.Name = updatePerson.Name;
            var result = await _personRepository.UpdatePerson(person);
            if (result > 0)
            {
                response.StatusCode = StatusCodes.Status201Created;
                response.Message = "Successfully updated person";
                return StatusCode(StatusCodes.Status201Created, response);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "failed to upate person");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{user_id}")]
        public async Task<IActionResult> Delete(string user_id)
        {
            ApiResponse response = new();

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            if (user_id == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "id cannot be zero";
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            var result = await _personRepository.DeletePerson(user_id);
            if (result > 0)
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Successfully deleted person";
                return StatusCode(StatusCodes.Status200OK, response);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "failed to delete person");
        }

    }
}
