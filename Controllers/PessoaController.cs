using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
// using Csharp_W_CSF.Models;

//http://zetcode.com/csharp/mysql/
// on command line type it:
// $ dotnet add package MySql.Data
// for access MySql ADO.NET framerwork. Include the package to our .NET Core project.

namespace Csharp_W_CSF_and_CSF_Learn001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpGet("GetBd")] // http://localhost:5000/pessoa/getall RETORNA todos
        public IActionResult GetMySql()
        {
            try
            {
                return Ok(DB.BuscarNoBancoDados());
            }
            catch (Exception ex)
            {         
                return NotFound(ex.Message); // 404 não encontrado
            }
        }

        [HttpGet("GetOne/{cpf}")] // http://localhost:5000/pessoa/1 RETORNA O SEGUNDO DA LISTA
        public IActionResult GetSingle(string cpf)
        {            
            try
            {
                Pessoa p = new Pessoa { Cpf = cpf };
                return Ok(DB.GetPessoa(p));
            }
            catch (Exception ex)
            {         
                return NotFound(ex.Message); // 404 não encontrado
            }
        }        

        [HttpPost("AddBD")] // http://localhost:5000/pessoa/ NO POSTMAN, CLICA EM BODY, DEPOISCLICA EM RAW, E AGORA CLICA EM JSON. PREENCHE O NOVO DADO EM JSON, E ENVIAR
        public IActionResult AddPessoaBd(Pessoa newPessoa)
        {
            try
            {
                return Ok(DB.InserirNoBancoDados(newPessoa));
            }
            catch (Exception ex)
            {             
                return NotFound(ex.Message); // 404 não encontrado
            }
        }

        [HttpPut("PutBd")]
        public IActionResult PutPessoaBd(Pessoa update)
        {
            try
            {                
                Pessoa pessoa = DB.GetPessoa(update);                
                Pessoa aux = DB.AtualizarPropriedades(update, pessoa);
                Pessoa atualizada = DB.AtualizarNoBancoDados(aux);
                return Ok(atualizada);
            }
            catch (Exception ex)
            {             
                return NotFound(ex.Message); // 404 não encontrado
            }            
        }

        [HttpDelete("bd/{cpf}")]
        public IActionResult DeletePessoaBd(string cpf)
        {
            try
            {
                return Ok(DB.ApagarNoBancoDados(cpf)); // retorna apenas 200 se ok OU lança exceção
            }
            catch (Exception ex)
            {         
                return NotFound(ex.Message); // 404 não encontrado
            }            
        }        
    }
}