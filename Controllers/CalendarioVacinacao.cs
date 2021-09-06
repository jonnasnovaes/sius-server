using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace sius_server.Controllers
{
    [ApiController]
    [Route("api/calendario-vacinacao")]
    public class CalendarioVacinacao : ControllerBase
    {

        [HttpGet]
        public string GetCalendarioVacinacao()
        {

            var calendarioVacinacao = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Seeds/calendarioVacinacao.json");
            return calendarioVacinacao;
        }
        
    }
}