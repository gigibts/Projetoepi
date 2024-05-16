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
    [Authorize]
    public class CadastroEpiController : ControllerBase
    {
        private readonly AppDbContext _context;
/// <summary>
/// 
/// </summary>
        public CadastroEpiController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna as Epis existentes
        /// </summary>
        /// <remarks>
        ///   Sample request:
        ///      Get do cadastro de epis são:
        ///      Nome_epi: "nome da epi",
        ///      descricao_uso: "passo a passo da utilização da epi" 
        /// </remarks>
        /// <returns></returns>
        // GET: api/CadastroEpi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadastroEpi>>> GetCadastroEpis()
        {
          if (_context.CadastroEpis == null)
          {
              return NotFound();
          }
            return await _context.CadastroEpis.ToListAsync();
        }
        /// <summary>
        /// Retorna os dados específicos
        /// </summary>
        /// <remarks>
        ///   Sample request:
        ///      Get ID específico do cadastro de epis são:
        ///      Nome_epi: "nome da epi",
        ///      descricao_uso: "passo a passo da utilização da epi" 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/CadastroEpi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CadastroEpi>> GetCadastroEpi(int id)
        {
          if (_context.CadastroEpis == null)
          {
              return NotFound();
          }
            var cadastroEpi = await _context.CadastroEpis.FindAsync(id);

            if (cadastroEpi == null)
            {
                return NotFound();
            }

            return cadastroEpi;
        }
        /// <summary>
        /// Edita os dados existentes
        /// </summary>
        /// <remarks>
        ///   Sample request:
        ///      Put do cadastro editado de epis são:
        ///      Nome_epi: "nome da epi",
        ///      descricao_uso: "passo a passo da utilização da epi" 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="cadastroEpi"></param>
        /// <returns></returns>
        // PUT: api/CadastroEpi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCadastroEpi(int id, CadastroEpi cadastroEpi)
        {
            if (id != cadastroEpi.CodEpi)
            {
                return BadRequest();
            }

            _context.Entry(cadastroEpi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastroEpiExists(id))
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
        /// Posta um epi novo
        /// </summary>
        /// <remarks>
        ///   Sample request:
        ///      Post do cadastro de novos epis são:
        ///      Nome_epi: "nome da epi",
        ///      descricao_uso: "passo a passo da utilização da epi" 
        /// </remarks>
        /// <param name="cadastroEpi"></param>
        /// <returns></returns>
        // POST: api/CadastroEpi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CadastroEpi>> PostCadastroEpi(CadastroEpi cadastroEpi)
        {
          if (_context.CadastroEpis == null)
          {
              return Problem("Entity set 'AppDbContext.CadastroEpis'  is null.");
          }
            _context.CadastroEpis.Add(cadastroEpi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCadastroEpi", new { id = cadastroEpi.CodEpi }, cadastroEpi);
        }
        /// <summary>
        /// Deleta a epi existente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/CadastroEpi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastroEpi(int id)
        {
            if (_context.CadastroEpis == null)
            {
                return NotFound();
            }
            var cadastroEpi = await _context.CadastroEpis.FindAsync(id);
            if (cadastroEpi == null)
            {
                return NotFound();
            }

            _context.CadastroEpis.Remove(cadastroEpi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CadastroEpiExists(int id)
        {
            return (_context.CadastroEpis?.Any(e => e.CodEpi == id)).GetValueOrDefault();
        }
    }
}
