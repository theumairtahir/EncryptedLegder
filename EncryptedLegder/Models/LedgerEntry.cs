using System;

namespace EncryptedLegder.Models
{
    public class LedgerEntry<TransactioneeIdType>
    {
        public long PrimaryKey { get; set; }
        public TransactioneeIdType TransactioneeId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public override string ToString()
        {
            string s = "";
            foreach (var prop in GetType().GetProperties())
            {
                s += $"{prop.Name}: {prop.GetValue(this)}, ";
            }
            s = s.Substring(0, s.Length - 1);
            return s;
        }
    }
    public class EncryptedLedgerEntry<TransactioneeIdType>
    {
        public string PrimaryKey { get; set; }
        public TransactioneeIdType TransactioneeId { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }
        public string TransactionDateTime { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string Signature { get; set; }
        public override string ToString()
        {
            string s = "";
            foreach (var prop in GetType().GetProperties())
            {
                s += $"{prop.Name}: {prop.GetValue(this)}, ";
            }
            s = s.Substring(0, s.Length - 1);
            return s;
        }
    }
}
