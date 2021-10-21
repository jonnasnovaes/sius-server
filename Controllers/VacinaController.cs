using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IGenericRep<EstoqueVacina> _estoqueVacinaRep;
        private readonly DataContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _env;

        public VacinaController(IGenericRep<Vacina> vacinaRep, IGenericRep<SolicitarVacina> solicitarVacinaRep, IGenericRep<EstoqueVacina> estoqueVacinaRep, DataContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _vacinaRep = vacinaRep;
            _solicitarVacinaRep = solicitarVacinaRep;
            _estoqueVacinaRep = estoqueVacinaRep;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
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
        
        // Cadastra uma vacina a partir de um JSON
        //[HttpPost]
        //public async Task<IActionResult> PostVacina(Vacina vacina)
        //{
        //    var vacinaCriada = await _vacinaRep.CreateOne(vacina);
        //    
        //    // Insert na tabela de SolicitarVacina
        //    SolicitarVacina solicitarVacina = new SolicitarVacina();
        //    solicitarVacina.idVacina = vacina.Id;
        //    solicitarVacina.liberado = true;
        //    solicitarVacina.recebido = true;
        //    await _solicitarVacinaRep.CreateOne(solicitarVacina);
            
        //    //Insert na tabela de EstoqueVacina
        //    EstoqueVacina estoqueVacina = new EstoqueVacina();
        //    estoqueVacina.IdVacina = vacina.Id;
        //    estoqueVacina.Nome = vacina.Nome;
        //    estoqueVacina.Fabricante = vacina.Fabricante;
        //    estoqueVacina.Quantidade = 0;
        //    await _estoqueVacinaRep.CreateOne(estoqueVacina);
            
        //    return Ok(vacinaCriada);
        //}
        
        // Cadastra uma vacina a partir de um FormData
        [HttpPost("formData")]
        public async Task<IActionResult> PostVacinaFormData([FromForm] Vacina vacinaFormData)
        {

            var bulaName = "";
            if (_httpContextAccessor.HttpContext.Request.Form.Files["bula"] != null)
            {
                var fileBula = _httpContextAccessor.HttpContext.Request.Form.Files["bula"];
                bulaName = SaveBula(fileBula);
            }

            var vacina = vacinaFormData;
            vacina.Bula = bulaName;

            var vacinaCriada = await _vacinaRep.CreateOne(vacina);
            
            // Insert na tabela de SolicitarVacina
            SolicitarVacina solicitarVacina = new SolicitarVacina();
            solicitarVacina.idVacina = vacina.Id;
            solicitarVacina.liberado = true;
            solicitarVacina.recebido = true;
            await _solicitarVacinaRep.CreateOne(solicitarVacina);
            
            //Insert na tabela de EstoqueVacina
            EstoqueVacina estoqueVacina = new EstoqueVacina();
            estoqueVacina.IdVacina = vacina.Id;
            estoqueVacina.Nome = vacina.Nome;
            estoqueVacina.Fabricante = vacina.Fabricante;
            estoqueVacina.Quantidade = 0;
            await _estoqueVacinaRep.CreateOne(estoqueVacina);
            
            return Ok(vacinaCriada);
            
        }

        [HttpPost("many")]
        public async Task<IActionResult> PostVacinaMany(List<Vacina> vacina)
        {
            var vacinasCriadas = await _vacinaRep.CreateMany(vacina);

            var vacinasCriadasArray = vacinasCriadas.ToArray();

            foreach (var vacinaCriada in vacinasCriadasArray)
            {
                // Insert na tabela de SolicitarVacina
                SolicitarVacina solicitarVacina = new SolicitarVacina();
                solicitarVacina.idVacina = vacinaCriada.Id;
                solicitarVacina.liberado = true;
                solicitarVacina.recebido = true;
                await _solicitarVacinaRep.CreateOne(solicitarVacina);
            
                //Insert na tabela de EstoqueVacina
                EstoqueVacina estoqueVacina = new EstoqueVacina();
                estoqueVacina.IdVacina = vacinaCriada.Id;
                estoqueVacina.Nome = vacinaCriada.Nome;
                estoqueVacina.Fabricante = vacinaCriada.Fabricante;
                estoqueVacina.Quantidade = 0;
                await _estoqueVacinaRep.CreateOne(estoqueVacina);
            }

            return Ok(vacinasCriadas);
        }
        
        // Atualizar uma vacina a partir de um JSON
        //[HttpPut]
        //public async Task<IActionResult> PutVacina(Vacina vacina)
        //{
        //    var vacinaAtualizada = await _vacinaRep.EditOne(vacina);
        //    return Ok(vacinaAtualizada);
        // }
        
        
        // Atualizar uma vacina a partir de um objeto FormData
        [HttpPut("formData")]
        public async Task<IActionResult> PutVacinaFormData([FromForm] Vacina vacina)
        {
            var vacinaAtual = await _vacinaRep.GetOneById(vacina.Id);
            
            if (_httpContextAccessor.HttpContext.Request.Form.Files["bula"] != null)
            {
                var fileBula = _httpContextAccessor.HttpContext.Request.Form.Files["bula"];
                
                if (fileBula.FileName != vacinaAtual.Bula)
                {
                    var statusBula = RemoveBula(vacinaAtual.Bula);

                    if (statusBula)
                    {
                        SaveBula(fileBula);
                        vacinaAtual.Nome = vacina.Nome;
                        vacinaAtual.Fabricante = vacina.Fabricante;
                        vacinaAtual.DataFabricacao = vacina.DataFabricacao;
                        vacinaAtual.Bula = fileBula.FileName;
                        vacinaAtual.NumeroRegistro = vacina.NumeroRegistro;
                        await _context.SaveChangesAsync();
                    }
                }
                return Ok(vacinaAtual);
            }
            else
            {
                vacina.Bula = vacinaAtual.Bula;
                var vacinaAtualizada = await _vacinaRep.EditOne(vacina);
                return Ok(vacinaAtualizada);
            }
            
            
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacina(int id)
        {
            var solicitarVacinaItem = await _context.Set<SolicitarVacina>().FirstOrDefaultAsync(x => x.idVacina == id);
            _context.Set<SolicitarVacina>().Remove(solicitarVacinaItem);
            
            var vacinaDeletada = await _vacinaRep.DeleteOne(id);
            RemoveBula(vacinaDeletada.Bula);
            return Ok(vacinaDeletada);
        }
        
        // Função para salvar o PDF da bula
        private string SaveBula(IFormFile formFile)
        {   
            var dir = _env.ContentRootPath;

            var fileName = formFile.FileName;
            var extension = fileName.Split(".").Last();

            var fullName = Path.Combine(dir + "/wwwroot/bulas", fileName); 

            using(var fileStream = new FileStream(fullName, FileMode.Create))
            {   
                formFile.CopyTo(fileStream);
            }

            return fileName;
        }

        private bool RemoveBula(string bulaName)
        {
            var dir = _env.ContentRootPath;
            var fullName = Path.Combine(dir + "/wwwroot/bulas", bulaName);

            try
            {
                System.IO.File.Delete(fullName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        
    }
}