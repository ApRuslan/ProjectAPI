using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using planningsoverzicht.DB;
using planningsoverzicht.Entities;
using planningsoverzicht.Services;
using planningsoverzicht.ViewModels.WerknemerVM;

namespace planningsoverzicht.Controller
{
    [Route("[controller]/[action]")]
    public class WerknemerController : ControllerBase
    {
        private IWerknemerData _werknemerData;
        public WerknemerController(IWerknemerData werknemerDate)
        {
            _werknemerData = werknemerDate;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_werknemerData.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var model = _werknemerData.Get(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Create([FromBody] WerknemerCVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Werknemer newWerknemer = new Werknemer(
                model.Naam,
                model.Email,
                model.EigenGereserveerdeInfo,
                model.Beheerder,
                model.WeekVerwittiging,
                model.KalenderStandaardTeam
            );
            newWerknemer = _werknemerData.Add(newWerknemer);
            return CreatedAtAction(nameof(Details), new { newWerknemer.UserId }, newWerknemer);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WerknemerUVM model)
        {
            Werknemer werknemer = _werknemerData.Get(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            werknemer.Email = model.Email;
            werknemer.KalenderStandaardTeam = model.KalenderStandaardTeam;
            werknemer.Beheerder = model.Beheerder;
            werknemer.EigenGereserveerdeInfo = model.EigenGereserveerdeInfo;
            werknemer.WeekVerwittiging = model.WeekVerwittiging;
            _werknemerData.Update(werknemer);
            return AcceptedAtAction(nameof(Details), new { werknemer.UserId }, werknemer);
        }
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    _werknemerData.Delete(id);
        //    return Ok();
        //}

    }
}
