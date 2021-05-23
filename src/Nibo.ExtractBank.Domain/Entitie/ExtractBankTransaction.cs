using Nibo.ExtractBank.Domain.Enums;
using System;

namespace Nibo.ExtractBank.Domain.Entitie
{
    public class ExtractBankTransaction
    {
        
        //TRNTYPE
        public string Type { get; set; }

        //DTSERVER
        public DateTime Date { get; set; }

        //TRNAMT
        public decimal Amount { get; set; }
        
        //MEMO
        public string Description { get; set; }
        
    }
}
