using AutoMapper;
using Nibo.ExtractBank.Core.Util;
using Nibo.ExtractBank.Domain.Dto;
using Nibo.ExtractBank.Domain.Entitie;
using Nibo.ExtractBank.Domain.Interfaces.IRepository;
using Nibo.ExtractBank.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nibo.ExtractBank.Service.ExtractBankService
{
    public class ExtractBankService : IExtractBankService
    {
        private readonly IExtractBankRepository _extractBankRepository;

        private readonly IMapper _mapper;

        public ExtractBankService(IExtractBankRepository extractBankRepository, IMapper mapper)
        {
            _extractBankRepository = extractBankRepository;
            _mapper = mapper;
        }

        public async Task<bool> SendFiles(List<string> filePaths)
        {
            var listTransactions = new List<TransactionBank>();

            foreach (var fileLocation in filePaths) 
            {
                listTransactions.Concat(await GetTransactions(fileLocation, listTransactions)).ToList();
            }

            if (listTransactions.Count <= 0) 
            {
                return false;
            }

            return true;

        }

        private async Task<List<TransactionBank>> GetTransactions(string ofxFilePath, List<TransactionBank> listTransactions)
        {
            
            CheckFileExists(ofxFilePath);

            StreamReader sr = new StreamReader(ofxFilePath);

            var objTransaction = OfxConverterToXml.Parser(sr);

            foreach (var item in objTransaction.Transactions)
            {
                var transaction = new TransactionBank()
                {
                    CheckTransaction = objTransaction.CheckTransaction,
                    IdBank = objTransaction.IdBank,                    
                    InitDateTransaction = objTransaction.InitDateTransaction,
                    EndtDateTransaction = objTransaction.EndtDateTransaction,
                    
                    Type = item.Type,
                    Date = item.Date,
                    Description = item.Description,
                    Amount = item.Amount,
                };

                if ((await CheckIfTransacationExist(transaction)) == false)

                    listTransactions.Add(transaction);
            }

            var distincList = listTransactions.Distinct();

            if (distincList.Count() <= 0)
            {
                return distincList.ToList();
            }
            else 
            {
                await _extractBankRepository.AddRange(distincList.ToList());
            }
            
            return distincList.ToList();
        }

        private async Task<bool> CheckIfTransacationExist(TransactionBank transactionBanks)
        {   
            return await _extractBankRepository.CheckIfExistsTransaction(transactionBanks);

        }


        private static void CheckFileExists(string file)
        {
            if (!File.Exists(file)) 
            {
                throw new FileNotFoundException("OFX file not found: " + file);
            }
        }

        public async Task<List<TransactionBankDto>> GetAll()
        {
            var getAll = await _extractBankRepository.GetAll();

            return _mapper.Map<List<TransactionBankDto>>(getAll);

        }
    }
}