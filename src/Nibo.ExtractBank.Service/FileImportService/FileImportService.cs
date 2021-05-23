using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Nibo.ExtractBank.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Service.FileImportService
{
    public class FileImportService : IFileImportService
    {

        private readonly IExtractBankService _extractBankService;
        
        public FileImportService(IExtractBankService extractBankService)
        {
            _extractBankService = extractBankService;            
        }


        public async Task<bool> Upload(List<IFormFile> files)
        {
            var filePaths = new List<string>();

            files.ForEach(file =>
            {
                if (file.Length <= 0) return;
                var fileName = file.FileName;
                var path = Path.Combine(fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                    filePaths.Add(path);
                }
            });

            if (filePaths.Count > 0) 
            {
                var result = await _extractBankService.SendFiles(filePaths);
                if (result) return true;
            }
            return false;
        }
    }
}
