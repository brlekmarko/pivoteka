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
public class VrstumController : ControllerBase
{
    private readonly IVrstumRepository<string, DbModels.Vrstum> _vrstumRepository;

    public VrstumController(IVrstumRepository<string, DbModels.Vrstum> vrstumRepository)
    {
        _vrstumRepository = vrstumRepository;
    }

    // GET: api/Vrstum
    [HttpGet]
    public ActionResult<IEnumerable<Vrstum>> GetAllVrstum()
    {
        return Ok(_vrstumRepository.GetAll().Select(DtoMapping.ToDto));
    }

    // GET: api/Vrstum/5
    [HttpGet("{ime}")]
    public ActionResult<Vrstum> GetVrstum(string ime)
    {
        var vrstumOption = _vrstumRepository.Get(ime).Map(DtoMapping.ToDto);

        return vrstumOption
            ? Ok(vrstumOption.Data)
            : NotFound();
    }

    // PUT: api/Vrstum/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPut("{ime}")]
    public IActionResult EditVrstum(string ime, Vrstum vrstum)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (ime != vrstum.Ime)
        {
            return BadRequest();
        }

        if (!_vrstumRepository.Exists(ime))
        {
            return NotFound();
        }

        return _vrstumRepository.Update(vrstum.ToDbModel())
            ? AcceptedAtAction("EditVrstum", vrstum)
            : StatusCode(500);
    }

    // POST: api/Vrstum
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPost]
    public ActionResult<Vrstum> CreateVrstum(Vrstum vrstum)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return _vrstumRepository.Insert(vrstum.ToDbModel())
            ? CreatedAtAction("GetVrstum", new { ime = vrstum.Ime }, vrstum)
            : StatusCode(500);
    }

    // DELETE: api/Vrstum/5
    [HttpDelete("{ime}")]
    public IActionResult DeleteVrstum(string ime)
    {
        if (!_vrstumRepository.Exists(ime))
            return NotFound();

        return _vrstumRepository.Remove(ime)
            ? NoContent()
            : StatusCode(500);
    }
}