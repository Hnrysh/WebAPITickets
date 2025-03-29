using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ContextoBD _contexto;

        public UsuariosController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            _contexto.Usuarios.Add(usuarios);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction("GetUsuarios", new { id = usuarios.us_identificador }, usuarios);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.us_identificador)
            {
                return BadRequest();
            }
            _contexto.Entry(usuarios).State = EntityState.Modified;
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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
        private bool UsuariosExists(int id)
        {
            return _contexto.Usuarios.Any(e => e.us_identificador == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuarios>> DeleteUsuarios(int id)
        {
            var usuarios = await _contexto.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            _contexto.Usuarios.Remove(usuarios);
            await _contexto.SaveChangesAsync();
            return usuarios;
        }
    }
}
