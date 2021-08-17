using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemoReusableFramework.HelperClass
{
    public class APIHelper<T>
    {
        public RestClient client;
        public RestRequest request;
        public string baseURI = ConfigurationManager.AppSettings["hostName"].ToString();
    
        public RestClient SetURL(string resource)
        {
            var endpoint = Path.Combine(baseURI, resource);
            var client = new RestClient(endpoint);
            return client;
        }

        public RestRequest GameBalancePostRequest(dynamic payload,string coreID,string forward,int clientTypeId)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(payload);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("CorrelationId", coreID);
            request.AddHeader("Forwarded-For",forward );
            request.AddHeader("Clienttypeid",clientTypeId.ToString());
            return request;
        }
       
        public RestRequest CreatePostRequest(dynamic payload,string coreID,int clientTypeId)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(payload);
            //request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("CorrelationId", coreID);
            request.AddHeader("Clienttypeid", clientTypeId.ToString());
            request.AddHeader("ProductId",ConfigurationManager.AppSettings["productId"].ToString());
            request.AddHeader("ModuleId",ConfigurationManager.AppSettings["moduleId"].ToString());
            request.AddHeader("Authorization", "Bearer "+GameDemoReusableFramework.Properties.Settings.Default.UserToken.ToString());
            return request;
        }
         public IRestResponse GetResponse(RestClient client,RestRequest request)
        {
            return client.Execute(request);
        }
    
        public string Serialize(dynamic payload)
        {
            string serializeObject = JsonConvert.SerializeObject(payload, Formatting.Indented);
            return serializeObject;
        }
        public DTO GetContent<DTO>(IRestResponse response)
        {
           DTO data = new JsonDeserializer().Deserialize<DTO>(response);
            return data;
        }
    }
}
