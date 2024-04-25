using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class EntregaEpiController : ControllerBase
    {
        private readonly AppDbContext _context;
/// <summary>
/// 
/// </summary>

        public EntregaEpiController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna a entrega existente
        /// </summary>
        /// <remarks>
        ///   Sample request:
        ///      Get do cadastro das entregas de epis são:
        ///      cod_entrega: "codigo da entrega da epi",
        ///      data_validade: "data de validade da epi",
        ///      cod_colaborador: "codigo do colaborador",
        ///      cod_epi: "codigdo da epi",
        ///      data_entrega: "data de entrega da epi"
        /// </remarks>
        /// <returns></returns>
        // GET: api/EntregaEpi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntregaEpi>>> GetEntregaEpis()
        {
          if (_context.EntregaEpis == null)
          {
              return NotFound();
          }
            return await _context.EntregaEpis.ToListAsync();
        }
        /// <summary>
        /// Retorna a entrega específica
        /// </summary>
        /// <remarks>
        ///   Sample request:
        ///      Get do cadastro das entregas especificas de epis são:
        ///      cod_entrega: "codigo da entrega da epi",
        ///      data_validade: "data de validade da epi",
        ///      cod_colaborador: "codigo do colaborador",
        ///      cod_epi: "codigdo da epi",
        ///      data_entrega: "data de entrega da epi"
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/EntregaEpi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntregaEpi>> GetEntregaEpi(int id)
        {
          if (_context.EntregaEpis == null)
          {
              return NotFound();
          }
            var entregaEpi = await _context.EntregaEpis.FindAsync(id);

            if (entregaEpi == null)
            {
                return NotFound();
            }

            return entregaEpi;
        }
        /// <summary>
        /// Edita uma entrega existente
        /// </summary>
        /// /// <remarks>
        ///   Sample request:
        ///      Put do cadastro das entregas editadas de epis são:
        ///      cod_entrega: "codigo da entrega da epi",
        ///      data_validade: "data de validade da epi",
        ///      cod_colaborador: "codigo do colaborador",
        ///      cod_epi: "codigdo da epi",
        ///      data_entrega: "data de entrega da epi"
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="entregaEpi"></param>
        /// <returns></returns>
        // PUT: api/EntregaEpi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntregaEpi(int id, EntregaEpi entregaEpi)
        {
            if (id != entregaEpi.CodEntrega)
            {
                return BadRequest();
            }

            _context.Entry(entregaEpi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaEpiExists(id))
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
        /// Posta uma nova entrega
        /// </summary>
        /// /// <remarks>
        ///   Sample request:
        ///      POst do cadastro das entregas novas de epis são:
        ///      cod_entrega: "codigo da entrega da epi",
        ///      data_validade: "data de validade da epi",
        ///      cod_colaborador: "codigo do colaborador",
        ///      cod_epi: "codigdo da epi",
        ///      data_entrega: "data de entrega da epi"
        /// </remarks>
        /// <param name="entregaEpi"></param>
        /// <returns></returns>
        // POST: api/EntregaEpi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntregaEpi>> PostEntregaEpi(EntregaEpi entregaEpi)
        {
          if (_context.EntregaEpis == null)
          {
              return Problem("Entity set 'AppDbContext.EntregaEpis'  is null.");
          }
            _context.EntregaEpis.Add(entregaEpi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntregaEpi", new { id = entregaEpi.CodEntrega }, entregaEpi);
        }
        /// <summary>
        /// Deleta a entrega existente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/EntregaEpi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntregaEpi(int id)
        {
            if (_context.EntregaEpis == null)
            {
                return NotFound();
            }
            var entregaEpi = await _context.EntregaEpis.FindAsync(id);
            if (entregaEpi == null)
            {
                return NotFound();
            }

            _context.EntregaEpis.Remove(entregaEpi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaEpiExists(int id)
        {
            return (_context.EntregaEpis?.Any(e => e.CodEntrega == id)).GetValueOrDefault();
        }
    }
}
