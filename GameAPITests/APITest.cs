using GameDemoReusableFramework;
using GameDemoReusableFramework.HelperClass;
using GameDemoReusableFramework.RequestModels;
using GameDemoReusableFramework.ResponseModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GameAPITests
{   


    [TestFixture]
    public class APITest
    {

        public string CoreID
        {
            get; set;
        }
        public int ClientID
        {
            get;set;
        }
       
        [Test, Order(1)]
        public void VerifyGameBalanceResponse()
        {   
            // declaration of Variables and set values to Request Model
            string resouce = "accounts/login/real";
            CoreID = "test123";
            ClientID = 5;
            string forwaredFor = "test12345";
            var test = new Tests<GameBalanceResponse>();
            var gameBReq = new GameBalanceRequest();       
            gameBReq.UserName = ConfigurationManager.AppSettings["userName"].ToString();
            gameBReq.Password = ConfigurationManager.AppSettings["passWord"].ToString();
            gameBReq.SessionProductId = int.Parse(ConfigurationManager.AppSettings["productId"].ToString());
            gameBReq.NumLaunchTokens = int.Parse(ConfigurationManager.AppSettings["numLaunchTokens"].ToString());
            gameBReq.MarketType = ConfigurationManager.AppSettings["market"].ToString();
            gameBReq.Environment.LanguageCode = ConfigurationManager.AppSettings["langCode"].ToString();
            gameBReq.Environment.ClientTypeId = ClientID;

            //Calling Post request for finding Game Balance, returning deserialize data
            var data = test.GameBalancePost(resouce,ClientID,CoreID, forwaredFor,gameBReq);
          
            // Storing USer token in project settings property and saving it for future use
            GameDemoReusableFramework.Properties.Settings.Default.UserToken = data.Tokens.UserToken[0].ToString();
            GameDemoReusableFramework.Properties.Settings.Default.Save();
            Console.WriteLine("Total Balance is {0}", data.Account.Balance.TotalInAccountCurrency);
           
            // asserting User token and CurrenctISOcode.
            Assert.AreEqual("JYDFGDFGFDGRTMPWZDFDFG", data.Tokens.UserToken[0].ToString(), string.Format("User token is not as expected. Actual user token is {0}", data.Tokens.UserToken[0].ToString()));
            Assert.AreEqual("USD", data.Account.Core.CurrencyIsoCode.ToString(),"Currency ISO code is not USD");
        
        }

        [Test, Order(2)]
        public void VerifyGameBalanceAfterRefresh()
        {
            // Creating Variables and passing payload as normal Json format.
            ClientID = 38;
            CoreID = "93D10259-30F8-4339-B456-3F30A43F65A2";
            string moduleID = ConfigurationManager.AppSettings["moduleId"].ToString();
            string serverID = ConfigurationManager.AppSettings["serverId"].ToString();
            string payLoad = "{" +
                "\"packet\": {\"packetType\": 7,\"payload\": \"<Pkt version='6'><Id" +
                string.Format("mid='{0}' cid='{1}' sid='{2}' sessionid='' verb='AdvSlot' clientLang='en'/><Request verbex='Refresh'/>",ClientID,moduleID,serverID) + 
                "</Pkt>\",\"useFilter\": true,\"isBase64Encoded\": false}}";
            string resource = string.Format("games/module/{0}/client/{1}/play",moduleID,ClientID);
            var tests = new Tests<PostResponse>();

            // calling refreshPost request and returning deserialized json data.
            var responseData = tests.RefreshPostRequest(resource, payLoad,ClientID,CoreID);
            Console.WriteLine("FinancialBalance is combination of Bet amount is {0} and Payout amount is {1}", responseData.Context.Financials.BetAmount, responseData.Context.Financials.PayoutAmount);
            
            // Storing Financial balance and player balance in Project setting properties and saving it for future use.
            GameDemoReusableFramework.Properties.Settings.Default.FinancialBal = responseData.Context.Financials.BetAmount + responseData.Context.Financials.PayoutAmount;
            Console.WriteLine("Player Balance is combination of loyalty balance {0} and TotalInAccountCurrency {1}", responseData.Context.Balances.LoyaltyBalance, responseData.Context.Balances.TotalInAccountCurrency);
            GameDemoReusableFramework.Properties.Settings.Default.PlayerBal = responseData.Context.Balances.LoyaltyBalance + responseData.Context.Balances.TotalInAccountCurrency;
            GameDemoReusableFramework.Properties.Settings.Default.Save();
            Console.WriteLine("Financial Balance is {0}", GameDemoReusableFramework.Properties.Settings.Default.FinancialBal);
            Console.WriteLine("Player Balance is {0}", GameDemoReusableFramework.Properties.Settings.Default.PlayerBal);


            //Asserting Financial Bal and Player bal is numeric or not.
            Assert.AreEqual(typeof(long), (responseData.Context.Balances.LoyaltyBalance + responseData.Context.Balances.TotalInAccountCurrency).GetType(), "Player balane is not a Integer");
            Assert.AreEqual(typeof(long), (responseData.Context.Financials.BetAmount + responseData.Context.Financials.PayoutAmount).GetType(), "Financial balane is not a Integer");

        }


    }

}

