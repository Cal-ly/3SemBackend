﻿using Microsoft.AspNetCore.Mvc;
using _3SemLibrary;

namespace _3SemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly ParticipantsRepository _repository;

        public ParticipantController(ParticipantsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all participants.
        /// </summary>
        /// <returns>A list of participants.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Participant>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        /// <summary>
        /// Gets a participant by ID.
        /// </summary>
        /// <param name="id">The ID of the participant.</param>
        /// <returns>The participant with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Participant> GetById(int id)
        {
            try
            {
                var participant = _repository.GetById(id);
                return Ok(participant);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Adds a new participant.
        /// </summary>
        /// <param name="participant">The participant to add.</param>
        /// <returns>The created participant.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Participant> Add([FromBody] Participant participant)
        {
            try
            {
                participant.ValidateParticipant();
                _repository.Add(participant);
                return CreatedAtAction(nameof(GetById), new { id = participant.Id }, participant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing participant.
        /// </summary>
        /// <param name="id">The ID of the participant to update.</param>
        /// <param name="participant">The updated participant.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(int id, [FromBody] Participant participant)
        {
            try
            {
                participant.ValidateParticipant();
                _repository.Update(id, participant);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a participant by ID.
        /// </summary>
        /// <param name="id">The ID of the participant to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _repository.Remove(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}