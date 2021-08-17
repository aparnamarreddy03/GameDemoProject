using GameDemoReusableFramework.HelperClass;
using GameDemoReusableFramework.RequestModels;
using GameDemoReusableFramework.ResponseModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemoReusableFramework
{
    public class Tests<T>
    {

        public GameBalanceResponse GameBalancePost(string resouce,int clientTypeId,string correlationId,string forwaredFor,dynamic body)
        {
           
            var apiHelper = new APIHelper<GameBalanceResponse>();
            var client = apiHelper.SetURL(resouce);
            string payload = apiHelper.Serialize(body);
            var request = apiHelper.GameBalancePostRequest(payload, correlationId, clientTypeId,forwaredFor);
            var response = apiHelper.GetResponse(client, request);
            Console.WriteLine("Response Content is {0}", response.Content);
           
            
            //    if(response.IsSuccessful)
            //    {
            //        Console.WriteLine("Staus code is {0}", (int)response.StatusCode);
            //        Console.WriteLine("Successfully post request is executed");
                    
            //    }
            
            //else
            //{
            //    Console.WriteLine(response.ErrorMessage);
            //    Console.WriteLine(response.ErrorException);
                
            //}
            var content = apiHelper.GetContent<GameBalanceResponse>(response);
            return content;

        }

        public PostResponse RefreshPostRequest(string resouce,string payload,int clientID,string coreID)
        {
            var apiHelper = new APIHelper<PostResponse>();
            var client = apiHelper.SetURL(resouce);
            var request = apiHelper.GameBalancePostRequest(payload,coreID,clientID);
            var response = apiHelper.GetResponse(client, request);
            Console.WriteLine("Response Content is {0}", response.Content);
            var content = apiHelper.GetContent<PostResponse>(response);
            return content;
        }
    }
}
