using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ValetaxTest.Model;

namespace ValetaxTest.Controllers
{
    public class MJournalController : Controller
    {
        private readonly ValetaxDBContext _dbContext;
        public MJournalController(ValetaxDBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        [Route("api.user.journal.getRange")]
        [HttpPost]
        [ProducesResponseType(typeof (List<MJournal>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRange([FromBody] dynamic obj)
        {
            await Task.Delay(1000);
            return Ok(new List<MJournal>());
        }

        [Route("api.user.journal.getSingle")]
        [HttpPost]
        [ProducesResponseType(typeof(MJournal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery][Required] long id)
        {
            var result = await _dbContext.MJournals.FindAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

    }
}
