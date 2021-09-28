using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sius_server.Data;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    [ApiController]
    [Route("api/vacina")]
    public class VacinaController : ControllerBase
    {
        private readonly IGenericRep<Vacina> _vacinaRep;
        private readonly IGenericRep<SolicitarVacina> _solicitarVacinaRep;
        private readonly DataContext _context;

        public VacinaController(IGenericRep<Vacina> vacinaRep, IGenericRep<SolicitarVacina> solicitarVacinaRep, DataContext context)
        {
            _vacinaRep = vacinaRep;
            _solicitarVacinaRep = solicitarVacinaRep;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVacina()
        {
            var vacina = await _vacinaRep.GetAll();
            return Ok(vacina);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacinaById(int id)
        {
            var vacina = await _vacinaRep.GetOneById(id);
            return Ok(vacina);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostVacina(Vacina vacina)
        {
            var vacinaCriada = await _vacinaRep.CreateOne(vacina);
            
            SolicitarVacina solicitarVacina = new SolicitarVacina();
            solicitarVacina.idVacina = vacina.Id;
            solicitarVacina.liberado = true;
                
            await _solicitarVacinaRep.CreateOne(solicitarVacina);
            
            return Ok(vacinaCriada);
        }
        
        [HttpPut]
        public async Task<IActionResult> PutVacina(Vacina vacina)
        {
            var vacinaAtualizada = await _vacinaRep.EditOne(vacina);
            return Ok(vacinaAtualizada);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacina(int id)
        {
            var solicitarVacinaItem = await _context.Set<SolicitarVacina>().FirstOrDefaultAsync(x => x.idVacina == id);
            _context.Set<SolicitarVacina>().Remove(solicitarVacinaItem);
            
            var vacinaDeletada = await _vacinaRep.DeleteOne(id);
            return Ok(vacinaDeletada);
        }
        
    }
}