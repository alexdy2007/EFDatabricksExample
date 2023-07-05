using System.Net.Http.Headers;
using EFBricks.API.Entities;
using EFBricks.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace EFBricks.Service.DatabricksAPI
{
    public class DatabricksApiService : IDatabricksApiService
    {
        private string endpoint = "";
        private string PAT = "";
        
        private HttpClient client;
        private readonly IConfiguration _config;
        public DatabricksApiService(IConfiguration config){

            _config = config ?? throw new ArgumentNullException(nameof(config));
            endpoint = _config["dbrickSQL:hostEndpoint"];
            PAT = _config["dbrickSQL:PAT"];

            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {PAT}");
        }


        public async IAsyncEnumerable<BronzeAssetDTO> getBronzeAssets(string sql)
        {

            var requestData = new DatabricksAPIContent{statement=sql, warehouse_id="ead10bf07050390f", catalog="water_demos", schema="demo_app"};

            var response = await client.PostAsJsonAsync(endpoint, requestData);

            var responseString = await response.Content.ReadAsStringAsync();

            var jsonResponse = JObject.Parse(responseString);

            var status = jsonResponse["status"]["state"].ToString();

            if (status=="FAILED"){
                var message = jsonResponse["status"]["error"]["message"].ToString();

                Console.WriteLine($"query with sql,\n  {sql} \n failed to execture");
                throw new Exception($"query with sql,\n  {sql} \n failed to execture \n ${message}");
            }

            var results = jsonResponse["result"]["data_array"].ToString();

            var resultsArray = JsonConvert.DeserializeObject<List<List<string>>>(results);


            List<BronzeAssetDTO> entityList = new List<BronzeAssetDTO>();
            foreach (List<string> bronzeAsset in resultsArray) 
            {
                var bronzeEntity = BronzeAssetEntity.createFromDataAPI(bronzeAsset);
                yield return bronzeEntity;
            }

        }
        

        public string sanitiseSQLSelect(string sql)
        {

            // TODO MAKE MUCH MORE SOPHISTICATED
            string removeQuotesAroundTbls = sql.Replace("\"","");
            return removeQuotesAroundTbls;
            
        }

    }

    public class DatabricksAPIContent
    {
        public string statement {get; set;}
        public string warehouse_id {get; set;}

        public string catalog {get; set;}

        public string schema {get; set;}

    }

 
}