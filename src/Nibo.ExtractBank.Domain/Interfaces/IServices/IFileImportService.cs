using Microsoft.AspNetCore.Http;
using Nibo.ExtractBank.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Domain.Interfaces.IServices
{
    public interface IFileImportService
    {
        Task<bool> Upload(List<IFormFile> files);
                
    }
}
