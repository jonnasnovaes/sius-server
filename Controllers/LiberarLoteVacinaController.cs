using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sius_server.Data;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    
    [ApiController]
    [Route("api/liberar-lote")]
    
    public class LiberarLoteVacinaController : ControllerBase
    {
        private readonly IGenericRep<Vacina> _vacinaRep;
        private readonly IGenericRep<SolicitarVacina> _solicitarVacinaRep;
        private readonly DataContext _context;
        
        public LiberarLoteVacinaController(
            IGenericRep<Vacina> vacinaRep, IGenericRep<SolicitarVacina> solicitarVacinaRep, DataContext context)
        {
            _vacinaRep = vacinaRep;
            _solicitarVacinaRep = solicitarVacinaRep;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetLotesSolicitados()
        {
            var listaVacinasSolicitadasDb = await _solicitarVacinaRep.GetAll();
            var listaVacinasSolicitadas = listaVacinasSolicitadasDb.Where(l => l.liberado == false).ToArray();

            Vacina[] listaVacinas = new Vacina[listaVacinasSolicitadas.Length];
            if (listaVacinasSolicitadas.Length != 0)
            {
                for (var i = 0; i < listaVacinasSolicitadas.Length; i++)
                {
                    listaVacinas.SetValue(await _vacinaRep.GetOneById(listaVacinasSolicitadas[i].idVacina), i);
                }
                
            }
            return Ok(listaVacinas);
        }
        
        [HttpPut]
        public async Task<IActionResult> PutLiberarLote(SolicitarVacina solicitarVacina)
        {
            var vacina = await  _context.Set<SolicitarVacina>().FirstOrDefaultAsync(x => x.idVacina == solicitarVacina.idVacina);
            vacina.liberado = solicitarVacina.liberado;
            
            var vacinaSolicitada = await _solicitarVacinaRep.EditOne(vacina);
            
            return Ok(vacinaSolicitada);
        }
    }
}