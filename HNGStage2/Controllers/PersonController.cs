using Azure;
using HNGStage2.Models;
using HNGStage2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetPerson(string user_id) 
        {
            ApiResponse response = new();

            if (user_id == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "user_id cannot be null";
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            var person = await _personRepository.GetPerson(user_id);

            if (person == null)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = "person not found";
                return StatusCode(StatusCodes.Status404NotFound, response);
            }

            response.StatusCode = StatusCodes.Status200OK;
            response.Data = person;
            response.Message = "successfully retrieved person";
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonDto addPerson)
        {
            ApiResponse response = new();
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await _personRepository.AddPerson(addPerson);
            if (result != null)
            {
                response.StatusCode = StatusCodes.Status201Created;
                response.Message = "Successfully added person";
                response.Data = result;
                return StatusCode(StatusCodes.Status201Created, response);
            }
         
            return StatusCode(StatusCodes.Status500InternalServerError, "failed to add person");
        }

        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
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
            if (result != null)
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Successfully updated person";
                response.Data = result;
                return StatusCode(StatusCodes.Status200OK, response);
            }
            response.StatusCode = StatusCodes.Status500InternalServerError;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
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
