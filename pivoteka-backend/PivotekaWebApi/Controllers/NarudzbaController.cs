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
public class NarudzbaController : ControllerBase
{
    private readonly INarudzbaRepository<int, DbModels.Narudzba> _narudzbaRepository;
    private readonly INarucioPivoRepository<int, DbModels.NarucioPivo> _narucioPivoRepository;

    public NarudzbaController(INarudzbaRepository<int, DbModels.Narudzba> narudzbaRepository,
        INarucioPivoRepository<int, DbModels.NarucioPivo> narucioPivoRepository)
    {
        _narudzbaRepository = narudzbaRepository;
        _narucioPivoRepository = narucioPivoRepository;
    }

    // GET: api/Narudzba
    [HttpGet]
    public ActionResult<IEnumerable<Narudzba>> GetAllNarudzba()
    {
        return Ok(_narudzbaRepository.GetAll().Select(DtoMapping.ToDto));
    }

    // GET: api/Narudzba/aggregate
    [HttpGet("Aggregates")]
    public ActionResult<IEnumerable<NarudzbaAggregate>> GetAllNarudzbaAggregate()
    {
        return Ok(_narudzbaRepository.GetAllAggregates().Select(DtoMapping.ToAggregateDto));
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

    // GET: api/Narudzba/Aggregate/5
    [HttpGet("Aggregate/{id}")]
    public ActionResult<NarudzbaAggregate> GetNarudzbaAggregate(int id)
    {
        var narudzbaOption = _narudzbaRepository.GetAggregate(id).Map(DtoMapping.ToAggregateDto);
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

    // PUT: api/Narudzba/Aggregate/5
    [HttpPut("Aggregate/{id}")]
    public IActionResult EditNarudzbaAggregate(int id, NarudzbaAggregate narudzba)
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
        if (!_narucioPivoRepository.CheckAmount(narudzba.Stavke.Select(ns => ns.ToDbModel()).ToList()))
        {
            return BadRequest("Nema dovoljno piva na stanju");
        }

        if (_narudzbaRepository.UpdateAggregate(narudzba.ToDbModel()))
        {
            if(_narucioPivoRepository.ReduceAmount(narudzba.Stavke.Select(ns => ns.ToDbModel()).ToList()))
                return AcceptedAtAction("EditNarudzbaAggregate", narudzba);
        }
        return StatusCode(500);
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

    // POST: api/Narudzba/Aggregate
    [HttpPost("Aggregate")]
    public ActionResult<NarudzbaAggregate> CreateNarudzbaAggregate(NarudzbaAggregate narudzba)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!_narucioPivoRepository.CheckAmount(narudzba.Stavke.Select(ns => ns.ToDbModel()).ToList()))
        {
            return BadRequest("Nema dovoljno piva na stanju");
        }

        if(_narudzbaRepository.InsertAggregate(narudzba.ToDbModel()))
        {
            if(_narucioPivoRepository.ReduceAmount(narudzba.Stavke.Select(ns => ns.ToDbModel()).ToList()))
                return CreatedAtAction("GetNarudzba", new { id = narudzba.Id }, narudzba);
        }
        return StatusCode(500);
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