using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.ExtractBank.Domain.Dto
{
    public class TransactionBankDto : ExtractBankTransactionDto
    {
        public int IdBank { get; set; }

        //ACCTTYPE
        public string CheckTransaction { get; set; }

        //DTSTART
        public string InitDateTransaction { get; set; }

        //DTEND
        public string EndtDateTransaction { get; set; }

        public IList<TransactionBankDto> Transactions { get; set; }
    }
}
