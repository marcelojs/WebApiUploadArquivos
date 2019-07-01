using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadArquivoWebApi.Models;
using UploadArquivoWebAPI.Context;


namespace UploadArquivoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly ArquivoContext _arquivoContext;

        public ArquivoController(ArquivoContext arquivoContext)
        {
            _arquivoContext = arquivoContext;
        }

        // GET: api/Arquivo
        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public IEnumerable<Arquivo> Get()
        {
            var arquivos = _arquivoContext.Arquivo.ToList();
            return arquivos;
        }

        // GET: api/Arquivo/5 (NÃO SERÁ USADO)
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Arquivo (NÃO SERÁ USADO)
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Arquivo/5 (NÃO SERÁ USADO)
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _arquivoContext.Remove(_arquivoContext.Arquivo.Find(id));
        }

        [HttpPost, DisableRequestSizeLimit]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string dataCriacao = "";
                var keys = Request.Form.Keys;

                foreach (var key in keys)
                {
                    dataCriacao = key.ToString();
                }

                var folderName = Path.Combine("Resources", "Files");
                var pathSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    string fileName = file.FileName.Trim();
                    var fullPath = Path.Combine(pathSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    
                    string Hash = "";


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        Hash = ConverterBytesParaString(stream);
                    }

                    _arquivoContext.Add(new Arquivo(file.FileName, file.Length.ToString(), dataCriacao, Hash));
                    _arquivoContext.SaveChanges();
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception erro)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public string ConverterBytesParaString(FileStream fileStream)
        {
            MD5 _MD5 = MD5.Create();

            var bytes = _MD5.ComputeHash(fileStream);
            string hash = "";

            foreach (var bt in bytes)
            {
                hash += bt.ToString("x2");
            }

            return hash;
        }


    }
}
