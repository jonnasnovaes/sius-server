using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sius_server.Data.Interfaces;
using sius_server.Models;

namespace sius_server.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IGenericRep<Login> _loginRep;

        public LoginController(IGenericRep<Login> loginRep)
        {
            _loginRep = loginRep;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogin()
        {
            var login = await _loginRep.GetAll();
            return Ok(login);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogin(int id)
        {
            var loginUsuario = await _loginRep.GetOneById(id);
            return Ok(loginUsuario);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostLogin(Login login)
        {
            var loginList = await _loginRep.GetAll();
            var loginListArray = loginList.ToArray();

            if (loginListArray.Length != 0)
            {
                for (var i = 0; i < loginList.Count; i++)
                {
                    if (loginListArray[i].Email == login.Email && loginListArray[i].Senha == login.Senha)
                    {
                        return Ok(loginListArray[i]);
                    }
                }
            }
            else
            {
                return BadRequest("Nenhum perfil de usuário cadastrado");
            }

            return BadRequest("Não foi possível executar a rotina interna");
        }
        
        [HttpPost("new")]
        public async Task<IActionResult> PostNewLogin(Login login)
        {
            var loginCriado = await _loginRep.CreateOne(login);
            return Ok(loginCriado);
        }
        
        [HttpPost("new/many")]
        public async Task<IActionResult> PostNewLoginMany(List<Login> login)
        {
            var loginCriado = await _loginRep.CreateMany(login);
            return Ok(loginCriado);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var loginDeletado = await _loginRep.DeleteOne(id);
            return Ok(loginDeletado);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateLogin(Login login)
        {
            var loginEditado = await _loginRep.EditOne(login);
            return Ok(loginEditado);
        }
    }
}