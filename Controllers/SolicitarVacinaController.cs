using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    
    [ApiController]
    [Route("api/solicitar-vacina")]
    
    public class SolicitarVacinaController : ControllerBase
    {
        
        private readonly IGenericRep<SolicitarVacina> _solicitarVacinaRep;
        
        public SolicitarVacinaController(IGenericRep<SolicitarVacina> solicitarVacinaRep)
        {
            _solicitarVacinaRep = solicitarVacinaRep;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSolicitarVacina()
        {
            var listaSolicitacoes = await _solicitarVacinaRep.GetAll();
            return Ok(listaSolicitacoes);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostSolicitarVacina(SolicitarVacina solicitarVacina)
        {
            var vacinaSolicitada = await _solicitarVacinaRep.CreateOne(solicitarVacina);
            return Ok(vacinaSolicitada);
        }
        
    }
}