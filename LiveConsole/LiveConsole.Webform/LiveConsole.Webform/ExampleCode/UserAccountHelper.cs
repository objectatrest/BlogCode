using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveConsole.Webform.ExampleCode
{
    public class AccountEntry
    {
        public decimal Amount { get; set; }
        public DateTime EnteredOn { get; set; }
    }

    public class UserAccountHelper
    {
        static List<AccountEntry> ledger = new List<AccountEntry> 
            { 
                new AccountEntry{ Amount=10m, EnteredOn = new DateTime(2012, 2, 15) },
                new AccountEntry{ Amount=-5m, EnteredOn = new DateTime(2012, 2, 17) }
            };

        public List<AccountEntry> GetUserEntries(string username)
        {
            return ledger;
        }

        public void AddUserEntry(string username, AccountEntry entry)
        {
            ledger.Add(entry);
        }
    }
}