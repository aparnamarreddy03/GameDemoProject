using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemoReusableFramework.ResponseModels
{
    public partial class PostResponse
    {
        
        public Context Context { get; set; }
        public Packet Packet { get; set; }
    }

    public partial class Context
    {
        public Financials Financials { get; set; }
        public Balances Balances { get; set; }
    }

    public partial class Balances
    {
        public long LoyaltyBalance { get; set; }
        public long TotalInAccountCurrency { get; set; }
    }

    public partial class Financials
    {
        public long BetAmount { get; set; }
        public long PayoutAmount { get; set; }
    }

    public partial class Packet
    {
        public string Payload { get; set; }
        public long PacketType { get; set; }
        public bool IsBase64Encoded { get; set; }
    }
}

