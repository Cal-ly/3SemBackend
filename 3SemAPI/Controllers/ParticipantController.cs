using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<Participant>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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