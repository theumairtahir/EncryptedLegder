using System;
using System.Collections.Generic;

namespace EncryptedLedgerExample
{
    public partial class Table
    {
        public long Id { get; set; }
        public int TransactioneeId { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string Signature { get; set; }
    }
}
