using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.ExtractBank.Domain.Dto
{
    public class ExtractBankTransactionDto
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
