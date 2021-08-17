using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemoReusableFramework.RequestModels
{
    public partial class GameBalanceRequest
    {

        public Environment Environment { get; set; } = new Environment();
        public string UserName { get; set; }
        public string Password { get; set; }
        public long SessionProductId { get; set; }
        public long NumLaunchTokens { get; set; }
        public string MarketType { get; set; }
    }

    public partial class Environment
    {
        public long ClientTypeId { get; set; }
        public string LanguageCode { get; set; }
    }

}
