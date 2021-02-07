using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.ExchangeTools.CoinbasePro.Common
{
    public enum HoldType
    {
        transfer,
        order
    }

    public enum EntryType
    {
        transfer,
        match,
        fee,
        rebate
    }

    public enum Cursor
    {
        None,
        Forward,
        Back
    }

    
}
