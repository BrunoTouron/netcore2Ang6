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
using Microsoft.AspNetCore.Identity;
using ApiCRM.Secutiry;

namespace ApiCRM.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUsersController(ApiContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ApplicationUsers
        [HttpGet]
        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            return _context.Users.OrderBy(x => x.Nome);
        }

        // GET: api/ApplicationUsers/filtro/nome
        [HttpGet("filtro/{nome}")]
        public IEnumerable<ApplicationUser> GetApplicationUsers([FromRoute] string nome)
        {
            if (nome == "null")
            {
                nome = "";
            }

            IEnumerable<ApplicationUser> resultList = _context.Users.
                                Where(x => ((nome.Length == 0) || (x.Nome.ToLower().Contains(nome.ToLower().Trim())))
                                        ).OrderBy(x => x.Nome);

            return resultList;
        }

        // GET: api/ApplicationUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aviso = await _context.Users.FindAsync(id);

            if (aviso == null)
            {
                return NotFound();
            }

            return Ok(aviso);
        }

        [HttpGet("exists/{userName}")]
        public bool Exists([FromRoute] string userName)
        {
            var lista = _context.Users.Where(x => x.UserName.ToLower().Equals(userName.ToLower())).ToList();

            if (lista.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // PUT: api/ApplicationUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            user.NormalizedUserName = user.UserName.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/ApplicationUsers
        [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            CreateUser(
                new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = true,
                    Perfil = "Administrador",
                    Nome = user.Nome
                }, "123mudar", Roles.ROLE_API_CRM);


            return CreatedAtAction("GetApplicationUser", new { id = user.Id }, user);
        }

        // DELETE: api/ApplicationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        //------------------------------------------------------

        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }

    }
}