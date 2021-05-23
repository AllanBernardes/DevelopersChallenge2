using Nibo.ExtractBank.Domain.Dto;
using Nibo.ExtractBank.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Domain.Interfaces.IServices
{
    public interface IExtractBankService
    {
        Task<bool> SendFiles(List<string> filePaths);

        Task<List<TransactionBankDto>> GetAll();
    }
}
