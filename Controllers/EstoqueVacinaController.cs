using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sius_server.Data;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    [ApiController]
    [Route("api/estoque-vacina")]
    public class EstoqueVacinaController : ControllerBase
    {
        
        private readonly IGenericRep<EstoqueVacina> _estoqueVacinaRep;
        // private readonly DataContext _context;
        
        public EstoqueVacinaController(IGenericRep<EstoqueVacina> estoqueVacinaRep, DataContext context)
        {
            _estoqueVacinaRep = estoqueVacinaRep;
            // _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEstoqueVacina()
        {
            var vacinasEstoque = await _estoqueVacinaRep.GetAll();
            return Ok(vacinasEstoque);
        }
        
    }
}