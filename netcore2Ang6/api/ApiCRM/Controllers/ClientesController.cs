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
    public class ClientesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ClientesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {
            return _context.Clientes.OrderBy(x => x.Nome);
            //return _context.Clientes.Include(m => m.Midia).OrderBy(x => x.Nome);
        }

        // GET: api/Clientes/nome/cpf/status
        [HttpGet("{nome}/{cpf}/{status}")]
        public IEnumerable<Cliente> GetClientes([FromRoute] string nome, string cpf, string status)
        {
            if (nome == "null")
            {
                nome = "";
            }

            if (cpf == "null")
            {
                cpf = "";
            }

            if (status == "null")
            {
                status = "";
            }

            //IEnumerable<Cliente> resultList = _context.Clientes.Include(m => m.Midia).
            IEnumerable < Cliente > resultList = _context.Clientes.
                Where(x => ((nome.Length == 0) || (x.Nome.ToLower().Contains(nome.ToLower().Trim())))
                        && ((cpf.Length == 0) || (x.CPF.Contains(cpf.Trim())))
                        //&& ((status.Length == 0) || (x.status = status))
                        ).OrderBy(x => x.Nome);

            return resultList;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var cliente = await _context.Clientes.FindAsync(id);
            var cliente = await _context.Clientes.Include(m => m.Midia).SingleOrDefaultAsync(p => p.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpGet("exists/{cpf}")]
        public bool Exists([FromRoute] string cpf)
        {
            var lista = _context.Clientes.Where(x => x.CPF.ToLower().Equals(cpf.ToLower())).ToList();

            if (lista.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente([FromRoute] int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}