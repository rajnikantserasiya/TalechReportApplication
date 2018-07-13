using FetchDataFromTalechPOS_BLL.TokenGeneralClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class MenuItems
    {
        List<AllMenuResultModel> lstAllMenuResultModel = new List<AllMenuResultModel>();

        public async Task<int> GetMenuItemsByCriteria(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation)
        {
            TokenDetails objTokenDetails = new TokenDetails();
            MenuItemsSearchCriteria objMenuItemsSearchCriteria =
             new MenuItemsSearchCriteria()
             {
                 storeid = 1,
                 offset = 0,
                 maxRecords = 1000,
                 searchString = "",
                 inventoryOnly = false,
                 //categoryId = 123343//546862
             };

            string jsonString = JsonConvert.SerializeObject(objMenuItemsSearchCriteria);
            JObject objInputParameters = JObject.Parse(jsonString);

            foreach (var obj in lstAllMerchantStoreInformation)
            {
                using (HttpClient client = new HttpClient())
                {
                    objTokenDetails.SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await objTokenDetails.
                        ExecuteClientPostMethod("managemenu/menuitem/allmenuitems", objInputParameters, client, Token.securityToken, obj.merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {

                        objTokenDetails.SetRefreshToken(response);
                        var objRes = await response.Content.ReadAsStringAsync();
                        JObject jsonRes = JObject.Parse(objRes);
                        JToken jTokenItemResult = jsonRes.FindTokens("items").FirstOrDefault();

                        foreach (var jObjectItem in jTokenItemResult.Children())
                        {
                            AllMenuResultModel objMenu = JsonConvert.DeserializeObject<AllMenuResultModel>(jObjectItem.ToString());
                            if (objMenu != null && !lstAllMenuResultModel.Any(s => s.name == objMenu.name && s.categoryType == objMenu.categoryType))
                                lstAllMenuResultModel.Add(objMenu);
                        }

                    }

                }

            }

            LogHelper.Log("Menu Item and Category method completed. Count: " + lstAllMenuResultModel.Count() + " Time: " + DateTime.Now);

            //DataTable dt = lstAllMenuResultModel.ToDataTable();
            return 0;

        }

        public async Task<int> GetMenuUpdatesByCriteria(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation)
        {
            TokenDetails objTokenDetails = new TokenDetails();
            MenuUpdatesSearchCriteria objMenuItemsSearchCriteria =
             new MenuUpdatesSearchCriteria()
             {
                 storeId = 1,
                 offset = 0,
                 maxRecords = 30
             };

            string jsonString = JsonConvert.SerializeObject(objMenuItemsSearchCriteria);
            JObject objInputParameters = JObject.Parse(jsonString);

            foreach (var obj in lstAllMerchantStoreInformation)
            {
                using (HttpClient client = new HttpClient())
                {
                    objTokenDetails.SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await objTokenDetails.ExecuteClientPostMethod("managemenu/getMenuUpdates", objInputParameters, client, Token.securityToken, obj.merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {

                        objTokenDetails.SetRefreshToken(response);
                        var objRes = await response.Content.ReadAsStringAsync();
                        //JObject jsonRes = JObject.Parse(objRes);
                        //JToken jTokenItemResult = jsonRes.FindTokens("items").FirstOrDefault();

                        //foreach (var jObjectItem in jTokenItemResult.Children())
                        //{
                        //    AllMenuResultModel objMenu = JsonConvert.DeserializeObject<AllMenuResultModel>(jObjectItem.ToString());
                        //    if (objMenu != null && !lstAllMenuResultModel.Any(s => s.name == objMenu.name && s.categoryType == objMenu.categoryType))
                        //        lstAllMenuResultModel.Add(objMenu);
                        //}

                    }

                }

            }

            //DataTable dt = lstAllMenuResultModel.ToDataTable();
            return 0;

        }

    }
}
