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
public class KorisnikController : ControllerBase
{
    private readonly IKorisnikRepository<string, DbModels.Korisnik> _korisnikRepository;

    public KorisnikController(IKorisnikRepository<string, DbModels.Korisnik> korisnikRepository)
    {
        _korisnikRepository = korisnikRepository;
    }

    // GET: api/Korisnik
    [HttpGet]
    public ActionResult<IEnumerable<Korisnik>> GetAllKorisnik()
    {
        return Ok(_korisnikRepository.GetAll().Select(DtoMapping.ToDto));
    }

    // GET: api/Korisnik/5
    [HttpGet("{ime}")]
    public ActionResult<Korisnik> GetKorisnik(string ime)
    {
        var korisnikOption = _korisnikRepository.Get(ime).Map(DtoMapping.ToDto);

        return korisnikOption
            ? Ok(korisnikOption.Data)
            : NotFound();
    }

    // PUT: api/Korisnik/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPut("{ime}")]
    public IActionResult EditKorisnik(string ime, Korisnik korisnik)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (ime != korisnik.Korisnicko_ime)
        {
            return BadRequest();
        }

        if (!_korisnikRepository.Exists(ime))
        {
            return NotFound();
        }

        return _korisnikRepository.Update(korisnik.ToDbModel())
            ? AcceptedAtAction("EditKorisnik", korisnik)
            : StatusCode(500);
    }

    // POST: api/Korisnik
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkime=2123754
    [HttpPost]
    public ActionResult<Korisnik> CreateKorisnik(Korisnik korisnik)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return _korisnikRepository.Insert(korisnik.ToDbModel())
            ? CreatedAtAction("GetKorisnik", new { ime = korisnik.Korisnicko_ime }, korisnik)
            : StatusCode(500);
    }

    // DELETE: api/Korisnik/5
    [HttpDelete("{ime}")]
    public IActionResult DeleteKorisnik(string ime)
    {
        if (!_korisnikRepository.Exists(ime))
            return NotFound();

        return _korisnikRepository.Remove(ime)
            ? NoContent()
            : StatusCode(500);
    }
}