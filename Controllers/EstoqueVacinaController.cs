using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly DataContext _context;
        
        public EstoqueVacinaController(IGenericRep<EstoqueVacina> estoqueVacinaRep, DataContext context)
        {
            _estoqueVacinaRep = estoqueVacinaRep;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEstoqueVacina()
        {
            var vacinasEstoque = await _estoqueVacinaRep.GetAll();
            return Ok(vacinasEstoque);
        }
        
        [HttpPut]
        public async Task<IActionResult> PutEstoqueVacina(EstoqueVacina estoqueVacina)
        {
            var vacinasEstoque = await _context.Set<EstoqueVacina>().FirstOrDefaultAsync(e => e.Id == estoqueVacina.Id);

            if (vacinasEstoque.Quantidade != 0)
            {
                vacinasEstoque.Quantidade--;
                var estoqueAtualizado = await _estoqueVacinaRep.EditOne(vacinasEstoque);
                return Ok(estoqueAtualizado);
            }
            
            return Problem("Não foi possível atualizar o estoque da vacina " + vacinasEstoque.Nome);
            
        }
        
        [HttpPut("editar")]
        public async Task<IActionResult> PutEditarEstoqueVacina(EstoqueVacina estoqueVacina)
        {
            try
            {
                var estoqueAtualizado = await _estoqueVacinaRep.EditOne(estoqueVacina);
                return Ok(estoqueAtualizado);
            }
            catch (Exception e)
            {
                return Problem("Não foi possível atualizar o estoque da vacina " + estoqueVacina.Nome);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEstoqueVacina(int id)
        {
            try
            {
                return Ok(await _estoqueVacinaRep.DeleteOne(id));
            }
            catch (Exception e)
            {
                return Problem("Não foi possível deletar a vacina do estoque");
            }
            
        }

    }
}