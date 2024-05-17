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
public class DobavljacController : ControllerBase
{
    private readonly IDobavljacRepository<string, DbModels.Dobavljac> _dobavljacRepository;

    public DobavljacController(IDobavljacRepository<string, DbModels.Dobavljac> dobavljacRepository)
    {
        _dobavljacRepository = dobavljacRepository;
    }

    // GET: api/Dobavljac
    [HttpGet]
    public ActionResult<IEnumerable<Dobavljac>> GetAllDobavljac()
    {
        return Ok(_dobavljacRepository.GetAll().Select(DtoMapping.ToDto));
    }

    // GET: api/Dobavljac/5
    [HttpGet("{ime}")]
    public ActionResult<Dobavljac> GetDobavljac(string ime)
    {
        var dobavljacOption = _dobavljacRepository.Get(ime).Map(DtoMapping.ToDto);

        return dobavljacOption
            ? Ok(dobavljacOption.Data)
            : NotFound();
    }

    // PUT: api/Dobavljac/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPut("{ime}")]
    public IActionResult EditDobavljac(string ime, Dobavljac dobavljac)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (ime != dobavljac.Ime)
        {
            return BadRequest();
        }

        if (!_dobavljacRepository.Exists(ime))
        {
            return NotFound();
        }

        return _dobavljacRepository.Update(dobavljac.ToDbModel())
            ? AcceptedAtAction("EditDobavljac", dobavljac)
            : StatusCode(500);
    }

    // POST: api/Dobavljac
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPost]
    public ActionResult<Dobavljac> CreateDobavljac(Dobavljac dobavljac)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return _dobavljacRepository.Insert(dobavljac.ToDbModel())
            ? CreatedAtAction("GetDobavljac", new { ime = dobavljac.Ime }, dobavljac)
            : StatusCode(500);
    }

    // DELETE: api/Dobavljac/5
    [HttpDelete("{ime}")]
    public IActionResult DeleteDobavljac(string ime)
    {
        if (!_dobavljacRepository.Exists(ime))
            return NotFound();

        return _dobavljacRepository.Remove(ime)
            ? NoContent()
            : StatusCode(500);
    }
}