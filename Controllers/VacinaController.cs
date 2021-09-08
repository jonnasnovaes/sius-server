using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    [ApiController]
    [Route("api/vacina")]
    public class VacinaController : ControllerBase
    {
        private readonly IGenericRep<Vacina> _vacinaRep;

        public VacinaController(IGenericRep<Vacina> vacinaRep)
        {
            _vacinaRep = vacinaRep;
        }

        [HttpGet]
        public async Task<IActionResult> GetVacina()
        {
            var vacina = await _vacinaRep.GetAll();
            return Ok(vacina);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostVacina(Vacina vacina)
        {
            var vacinaCriada = await _vacinaRep.CreateOne(vacina);
            return Ok(vacinaCriada);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacina(int id)
        {
            var vacinaDeletada = await _vacinaRep.DeleteOne(id);
            return Ok(vacinaDeletada);
        }
        
    }
}