using Autofac;
using EncryptedLegder.Injections;
using EncryptedLegder.Models;
using EncryptedLegder.Processes;
using EncryptedLegder.Abstractions;
using System;

namespace EncryptedLedgerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new LedgerInjection<int>("786", "1234567890123456").Inject();
            using var scope = container.BeginLifetimeScope();
            var transaction = scope.Resolve<ILedgerTransaction<int>>();
            var crypto = scope.Resolve<ICryptography>();
            var enc = transaction.DoATransactionOf(1000)
                        .From(1, 10000)
                        .To(2, 2000)
                        .On(DateTime.Now)
                        .DueTo("Other reason")
                        .With("abc")
                        .And("xyz")
                        .And("xyz2")
                        .And("xyz3")
                        .Done();
            foreach (var item in enc)
            {
                foreach (var line in item.ToString().Split(","))
                {
                    Console.WriteLine(line);
                }
            }
            foreach (var item in enc)
            {
                var dec = crypto.Decrypt<int>(item, out bool flag);
                foreach (var line in dec.ToString().Split(","))
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine($"verified: {flag}");
            }
            Console.ReadLine();
        }
    }
    class Model : LedgerEntry<int>
    {
        public Currencies Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ExchangedDebit
        {
            get
            {
                return ExchangeRate * Debit;
            }
        }
        public decimal ExchangedCredit
        {
            get
            {
                return ExchangeRate * Credit;
            }
        }
        public decimal ExchangedBalance
        {
            get
            {
                return ExchangeRate * Balance;
            }
        }
    }
    enum Currencies
    {
        Pkr,
        Dollar
    }
}
