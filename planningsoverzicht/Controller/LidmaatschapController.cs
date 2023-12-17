using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using planningsoverzicht.DB;
using planningsoverzicht.Entities;
using planningsoverzicht.Services;
using planningsoverzicht.ViewModels.LidmaatschapVM;
using System.Security.Permissions;

namespace planningsoverzicht.Controller
{
    [Route("[controller]/[action]")]
    public class LidmaatschapController : ControllerBase
    {
        private ILidmaatschapData _lidmaatschapData;
        public LidmaatschapController(ILidmaatschapData lidmaatschapData)
        {
            _lidmaatschapData = lidmaatschapData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_lidmaatschapData.GetAllLidmaatschappen());
        }
        [HttpPost]
        public IActionResult Create([FromBody] LidmaatschapCVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Lidmaatschap newLid = new Lidmaatschap(model.UserId, model.Rol, model.GevoeligeInfoBekijken, model.GereserveerdeInfoBekijken);
            _lidmaatschapData.AddLid(model.TeamId, newLid);
            return CreatedAtAction(nameof(Details), new { model.TeamId, newLid.UserId }, newLid);
        }
        [HttpPut("{teamId}/{lidId}")]
        public IActionResult Update(int teamId, int lidId, [FromBody] LidmaatschapUVM model)
        {
            Lidmaatschap lidmaatschap = _lidmaatschapData.Get(teamId, lidId);
            if (!ModelState.IsValid && lidmaatschap == null)
                return BadRequest(ModelState);
            lidmaatschap.Rol = model.Rol;
            lidmaatschap.GevoeligeInfoBekijken = model.GevoeligeInfoBekijken;
            lidmaatschap.GereserveerdeInfoBekijken = model.GereserveerdeInfoBekijken;
            _lidmaatschapData.UpdateLid(teamId,lidmaatschap);
            return Ok(lidmaatschap);
        }
        [HttpDelete("{teamId}/{lidId}")]
        public IActionResult Delete(int teamId, int lidId)
        {
            Lidmaatschap lidmaatschap = _lidmaatschapData.Get(teamId, lidId);
            if (lidmaatschap == null)
                return BadRequest("Lidmaatschap bestaat niet");
            _lidmaatschapData.DeleteLid(teamId, lidId);
            return Ok("Lidmaatschap is gedelete.");
        }
        [HttpGet("{id}")]
        public IActionResult ShowByUser(int id)
        {
            List<Team> Lid = _lidmaatschapData.GetByLid(id);
            if (Lid == null) return BadRequest(Lid);
            return Ok(Lid);
        }
        [HttpGet("{teamId}/{lidId}")]
        public IActionResult Details(int teamId, int lidId)
        {
            Lidmaatschap lid = _lidmaatschapData.Get(teamId, lidId);
            if (lid == null) return BadRequest(lid);
            return Ok(lid);
        }
    }
}
