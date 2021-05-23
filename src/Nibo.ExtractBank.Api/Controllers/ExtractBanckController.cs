using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nibo.ExtractBank.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractBanckController : ControllerBase
    {
        private readonly IFileImportService _fileImportService;
        private readonly IExtractBankService _extractBankService;

        public ExtractBanckController(IFileImportService fileImportService, IExtractBankService extractBankService)
        {
            _fileImportService = fileImportService;
            _extractBankService = extractBankService;
        }


        [HttpPost]
        [Route("import-extract-bank")]
        public IActionResult ImportExtractBank([FromForm(Name = "files")] List<IFormFile> files)
        {
            try
            {
                var fileLocations = _fileImportService.Upload(files);

                if (fileLocations.Result)
                {
                    
                    return StatusCode(StatusCodes.Status200OK, "O(s) arquivos foram importados com sucesso.");
                }
                else 
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Nenhuma transação foi salva");
                }
               
                
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpGet]
        [Route("getall-extract-bank")]
        public IActionResult GetAll()
        {
            try
            {
                var transactions = _extractBankService.GetAll();

                return Ok(transactions);
                
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}
