using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sius_server.Data;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    
    [ApiController]
    [Route("api/solicitar-vacina")]
    
    public class SolicitarVacinaController : ControllerBase
    {
        
        private readonly IGenericRep<SolicitarVacina> _solicitarVacinaRep;
        private readonly DataContext _context;
        
        public SolicitarVacinaController(IGenericRep<SolicitarVacina> solicitarVacinaRep, DataContext context)
        {
            _solicitarVacinaRep = solicitarVacinaRep;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSolicitarVacina()
        {
            var listaSolicitacoes = await _solicitarVacinaRep.GetAll();
            return Ok(listaSolicitacoes);
        }
        
        [HttpPut]
        public async Task<IActionResult> PutSolicitarVacina(SolicitarVacina solicitarVacina)
        {
            var vacinaSolicitada = await _context.Set<SolicitarVacina>().FirstOrDefaultAsync(x => x.idVacina == solicitarVacina.idVacina);
            vacinaSolicitada.liberado = solicitarVacina.liberado;
            vacinaSolicitada.recebido = solicitarVacina.recebido;
            
            var vacinaEditada = await _solicitarVacinaRep.EditOne(vacinaSolicitada);
            return Ok(vacinaEditada);
            
            
        }
        
    }
}