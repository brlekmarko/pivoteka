using Pivoteka.Repositories;
using Microsoft.AspNetCore.Mvc;
using PivotekaWebApi.DTOs;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Commons;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NarudzbaController : ControllerBase
{
    private readonly INarudzbaRepository<int, DbModels.Narudzba> _narudzbaRepository;

    public NarudzbaController(INarudzbaRepository<int, DbModels.Narudzba> narudzbaRepository)
    {
        _narudzbaRepository = narudzbaRepository;
    }

    // GET: api/Narudzba
    [HttpGet]
    public ActionResult<IEnumerable<Narudzba>> GetAllNarudzba()
    {
        return Ok(_narudzbaRepository.GetAll().Select(DtoMapping.ToDto));
    }

    // GET: api/Narudzba/5
    [HttpGet("{id}")]
    public ActionResult<Narudzba> GetNarudzba(int id)
    {
        var narudzbaOption = _narudzbaRepository.Get(id).Map(DtoMapping.ToDto);

        return narudzbaOption
            ? Ok(narudzbaOption.Data)
            : NotFound();
    }

    // PUT: api/Narudzba/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPut("{id}")]
    public IActionResult EditNarudzba(int id, Narudzba narudzba)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != narudzba.Id)
        {
            return BadRequest();
        }

        if (!_narudzbaRepository.Exists(id))
        {
            return NotFound();
        }

        return _narudzbaRepository.Update(narudzba.ToDbModel())
            ? AcceptedAtAction("EditNarudzba", narudzba)
            : StatusCode(500);
    }

    // POST: api/Narudzba
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPost]
    public ActionResult<Narudzba> CreateNarudzba(Narudzba narudzba)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return _narudzbaRepository.Insert(narudzba.ToDbModel())
            ? CreatedAtAction("GetNarudzba", new { id = narudzba.Id }, narudzba)
            : StatusCode(500);
    }

    // DELETE: api/Narudzba/5
    [HttpDelete("{id}")]
    public IActionResult DeleteNarudzba(int id)
    {
        if (!_narudzbaRepository.Exists(id))
            return NotFound();

        return _narudzbaRepository.Remove(id)
            ? NoContent()
            : StatusCode(500);
    }
}