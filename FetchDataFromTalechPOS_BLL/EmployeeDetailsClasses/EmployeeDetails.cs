using FetchDataFromTalechPOS_BLL.TokenGeneralClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL.EmployeeDetailsClasses
{
    public class EmployeeDetails
    {
        public async Task<List<MerchantIdentification_StoreName>> GetEmployeeByCriteria(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation)
        {
            TokenDetails objTokenDetails = new TokenDetails();

            EmployeeSearchCriteria objReportSearchCriteria = new EmployeeSearchCriteria();
            objReportSearchCriteria.searchCriteria = new EmpInputModel();
            objReportSearchCriteria.searchCriteria =
             new EmpInputModel()
             {
                 activeFilter = 1
             };

            string jsonString = JsonConvert.SerializeObject(objReportSearchCriteria);
            JObject objInputParameters = JObject.Parse(jsonString);

            List<EmployeeResultModel> lstEmployee = new List<EmployeeResultModel>();

            foreach (var obj in lstAllMerchantStoreInformation)
            {
                using (HttpClient client = new HttpClient())
                {
                    objTokenDetails.SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await objTokenDetails.
                        ExecuteClientPostMethod("manageemployee/getallemployees", objInputParameters, client, Token.securityToken, obj.merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {
                        objTokenDetails.SetRefreshToken(response);
                        var objRes = await response.Content.ReadAsStringAsync();

                        JObject jsonRes = JObject.Parse(objRes);
                        JToken jTokenUsers = jsonRes.FindTokens("users").FirstOrDefault();

                        foreach (JToken jobject in jTokenUsers.Children())
                        {
                            EmployeeResultModel objEmployee = JsonConvert.DeserializeObject<EmployeeResultModel>(jobject.ToString());
                            lstEmployee.Add(objEmployee);
                        }

                        // add employees in lstEmployee object
                        obj.lstEmployee = new List<EmployeeResultModel>();
                        obj.lstEmployee.AddRange(lstEmployee);
                    }

                }

            }

            return lstAllMerchantStoreInformation;

        }
    }
}
