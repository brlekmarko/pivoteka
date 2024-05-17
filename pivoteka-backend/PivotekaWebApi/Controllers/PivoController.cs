using Pivoteka.Repositories;
using Microsoft.AspNetCore.Mvc;
using PivotekaWebApi.DTOs;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Commons;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PivotekaWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PivoController : ControllerBase
{
    private readonly IPivoRepository<string, DbModels.Pivo> _pivoRepository;

    public PivoController(IPivoRepository<string, DbModels.Pivo> pivoRepository)
    {
        _pivoRepository = pivoRepository;
    }

    // GET: api/Pivo
    [HttpGet]
    public ActionResult<IEnumerable<Pivo>> GetAllPivo()
    {
        return Ok(_pivoRepository.GetAll().Select(DtoMapping.ToDto));
    }

    // GET: api/Pivo/5
    [HttpGet("{ime}")]
    public ActionResult<Pivo> GetPivo(string ime)
    {
        var pivoOption = _pivoRepository.Get(ime).Map(DtoMapping.ToDto);

        return pivoOption
            ? Ok(pivoOption.Data)
            : NotFound();
    }

    // PUT: api/Pivo/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPut("{ime}")]
    public IActionResult EditPivo(string ime, Pivo pivo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (ime != pivo.Ime)
        {
            return BadRequest();
        }

        if (!_pivoRepository.Exists(ime))
        {
            return NotFound();
        }

        return _pivoRepository.Update(pivo.ToDbModel())
            ? AcceptedAtAction("EditPivo", pivo)
            : StatusCode(500);
    }

    // POST: api/Pivo
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPost]
    public ActionResult<Pivo> CreatePivo(Pivo pivo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return _pivoRepository.Insert(pivo.ToDbModel())
            ? CreatedAtAction("GetPivo", new { ime = pivo.Ime }, pivo)
            : StatusCode(500);
    }

    // DELETE: api/Pivo/5
    [HttpDelete("{ime}")]
    public IActionResult DeletePivo(string ime)
    {
        if (!_pivoRepository.Exists(ime))
            return NotFound();

        return _pivoRepository.Remove(ime)
            ? NoContent()
            : StatusCode(500);
    }
}