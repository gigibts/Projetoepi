using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoepi.Context;
using projetoepi.Models;

namespace projetoepi.Controllers
{
/// <summary>
/// 
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;
/// <summary>
/// 
/// </summary>

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna o colaborador existente
        /// </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get do cadastro das epis são:
        ///      Nome: "nome da epi",
        ///      Ctps: "código da carteira de trabalho",
        ///      Cpf: "cpf do colaborador",
        ///      Telefone: "Telefone do colaborador",
        ///      Data_admissao: "Data de admissao da epi",
        ///      E-mail: "Email do colaborador"
        /// </remarks>
        /// <returns></returns>
        // GET: api/Colaborador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            return await _context.Colaboradors.ToListAsync();
        }
        /// <summary>
        /// Retorna o colaborador específico
        /// </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get ID específico do cadastro das epis são:
        ///      Nome: "nome da epi",
        ///      Ctps: "código da carteira de trabalho",
        ///      cpf: "cpf do colaborador",
        ///      telefone: "Telefone do colaborador",
        ///      data_admissao: "Data de admissao da epi",
        ///      email: "Email do colaborador"
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }
        /// <summary>
        /// Edita o colaborador existente
        /// </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Put do cadastro atualizado das epis são:
        ///      Nome: "nome da epi",
        ///      Ctps: "código da carteira de trabalho",
        ///      cpf: "cpf do colaborador",
        ///      telefone: "Telefone do colaborador",
        ///      data_admissao: "Data de admissao da epi",
        ///      email: "Email do colaborador"
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="colaborador"></param>
        /// <returns></returns>
        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.CodColaborador)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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
        /// <summary>
        /// Posta um novo colaborador
        /// </summary>
        /// <remarks>
        ///  Sample request:
        ///      Post do cadastro de novas epis são:
        ///      Nome: "nome da epi",
        ///      Ctps: "código da carteira de trabalho",
        ///      Cpf: "cpf do colaborador",
        ///      Telefone: "Telefone do colaborador",
        ///      Data_admissao: "Data de admissao da epi",
        ///      E-mail: "Email do colaborador"
        /// </remarks>
        /// <param name="colaborador"></param>
        /// <returns></returns>
        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
          if (_context.Colaboradors == null)
          {
              return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
          }
            _context.Colaboradors.Add(colaborador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColaborador", new { id = colaborador.CodColaborador }, colaborador);
        }
        /// <summary>
        /// Deleta um colaborador existente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Colaborador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.CodColaborador == id)).GetValueOrDefault();
        }
    }
}
