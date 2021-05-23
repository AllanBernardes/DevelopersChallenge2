using Nibo.ExtractBank.Domain.Entitie.BaseEntitie;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.ExtractBank.Domain.Entitie
{
    public class TransactionBank : Entity
    {
        
        //BANKID
        public int IdBank { get; set; }

        //ACCTTYPE
        public string CheckTransaction { get; set; }

        //DTSTART
        public string InitDateTransaction { get; set; }

        //DTEND
        public string EndtDateTransaction { get; set; }

        //TRNTYPE
        public string Type { get; set; }

        //DTSERVER
        public DateTime Date { get; set; }

        //TRNAMT
        public decimal Amount { get; set; }

        //MEMO
        public string Description { get; set; }

        //public IList<TransactionBank> Transactions { get; set; }

    }
}
