using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemoReusableFramework.ResponseModels
{
      
        public partial class GameBalanceResponse
        {
            public Account Account { get; set; }
            public Tokens Tokens { get; set; }
        }

        public partial class Account
        {
            public Core Core { get; set; }
            public AccountBalance Balance { get; set; }
        }

        public partial class AccountBalance
        {
            public long TotalInAccountCurrency { get; set; }
            public List<BalanceElement> Balances { get; set; }
            public List<PointBalance> PointBalances { get; set; }
            public bool IsBonusEnabled { get; set; }
            public bool IsExternalBonusEnabled { get; set; }
        }

        public partial class BalanceElement
        {
            public long TypeId { get; set; }
            public long AmountInAccountCurrency { get; set; }
        }

        public partial class PointBalance
        {
            public long TypeId { get; set; }
            public long Amount { get; set; }
        }

        public partial class Core
        {
            public string CurrencyIsoCode { get; set; }
            public bool IsExternalAccount { get; set; }
            public bool IsExternalBalance { get; set; }
            public string RegisteredProductId { get; set; }
            public long UserTypeId { get; set; }
            public long UserId { get; set; }
            public string Username { get; set; }
        }

        public partial class Tokens
        {
            public long UserTokenExpiryInSeconds { get; set; }
            public List<string> LaunchTokens { get; set; }
            public List<string> RefreshTokens { get; set; }
            public string UserToken { get; set; }
        }
    }



