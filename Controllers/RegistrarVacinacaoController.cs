using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sius_server.Data;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    
    [ApiController]
    [Route("api/registrar-vacinacao")]
    public class RegistrarVacinacaoController : ControllerBase
    {
        private readonly IGenericRep<RegistrarVacinacao> _registrarVacinacaoRep;
        private readonly DataContext _context;

        public RegistrarVacinacaoController(IGenericRep<RegistrarVacinacao> registrarVacinacaoRep, DataContext context)
        {
            _registrarVacinacaoRep = registrarVacinacaoRep;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetRegistrarVacinacao()
        {
            var pessoasVacinadas = await _registrarVacinacaoRep.GetAll();
            return Ok(pessoasVacinadas);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostRegistrarVacinacao(RegistrarVacinacao registrarVacinacao)
        {
            
            registrarVacinacao.DataVacinacao = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            
            var registroVacinacaoCriado = await _registrarVacinacaoRep.CreateOne(registrarVacinacao);
            return Ok(registroVacinacaoCriado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrarVacinacaoAll(int id)
        {
            return Ok(await _registrarVacinacaoRep.DeleteOne(id));
        }
    }
}