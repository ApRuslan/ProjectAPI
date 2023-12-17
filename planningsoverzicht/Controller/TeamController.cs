using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using planningsoverzicht.DB;
using planningsoverzicht.Entities;
using planningsoverzicht.Services;
using planningsoverzicht.ViewModels.LidmaatschapVM;
using planningsoverzicht.ViewModels.TeamVM;

namespace planningsoverzicht.Controller
{
    [Route("[controller]/[action]")]
    public class TeamController : ControllerBase
    {
        private ITeamData _teamData;
        public TeamController(ITeamData teamData) { _teamData = teamData; }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_teamData.GetAll());
        }
        [HttpPost]
        public IActionResult Create([FromBody] TeamCVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_teamData.GetByNaam(model.Naam) != null)
            {
                Team newTeam = new Team(model.Naam, model.Aanmaker);
                newTeam = _teamData.Add(newTeam);
                return CreatedAtAction(nameof(Details), new { newTeam.Id }, newTeam);
            }
            else return BadRequest("Team bestaat al.");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TeamUVM model)
        {
            bool changed = false;
            Team team = _teamData.Get(id);
            if (!ModelState.IsValid && team == null)
                return BadRequest(ModelState);
            if (model.Naam != "string")
            {
                team.Naam = model.Naam;
                changed = true;
            }
            if (model.Aanmaker != 0)
            {
                team.Aanmaker = model.Aanmaker;
                changed = true;
            }
            if (changed)
                return CreatedAtAction(nameof(Details), new { team.Id }, team);
            else return BadRequest("No Changes");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Team team = _teamData.Get(id);
            if (team == null)
                return BadRequest("Team bestaat niet.");
            _teamData.Delete(team);
            return Ok("Team deleted.");
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            Team team = _teamData.Get(id);
            if (team == null) return BadRequest(team);
            return Ok(team);
        }
        //#region Lidmaatschap
        //[Route("Lidmaatschap/")]
        
        //#endregion
    }
}
