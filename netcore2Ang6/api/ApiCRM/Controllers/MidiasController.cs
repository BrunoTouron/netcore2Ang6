using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiCRM.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MidiasController : ControllerBase
    {
        private readonly ApiContext _context;

        public MidiasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Midias
        [HttpGet]
        public IEnumerable<Midia> GetMidias()
        {
            return _context.Midias.OrderBy(x => x.Descricao); ;
        }

        [HttpGet("exists/{descricao}")]
        public bool Exists([FromRoute] string descricao)
        {
            var lista = _context.Midias.Where(x => x.Descricao.ToLower().Equals(descricao.ToLower())).ToList();

            if (lista.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: api/Midias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMidia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var midia = await _context.Midias.FindAsync(id);

            if (midia == null)
            {
                return NotFound();
            }

            return Ok(midia);
        }

        // PUT: api/Midias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMidia([FromRoute] int id, [FromBody] Midia midia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != midia.Id)
            {
                return BadRequest();
            }

            _context.Entry(midia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MidiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Midias
        [HttpPost]
        public async Task<IActionResult> PostMidia([FromBody] Midia midia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Midias.Add(midia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMidia", new { id = midia.Id }, midia);
        }

        // DELETE: api/Midias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMidia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var midia = await _context.Midias.FindAsync(id);
            if (midia == null)
            {
                return NotFound();
            }

            _context.Midias.Remove(midia);
            await _context.SaveChangesAsync();

            return Ok(midia);
        }

        private bool MidiaExists(int id)
        {
            return _context.Midias.Any(e => e.Id == id);
        }
    }
}