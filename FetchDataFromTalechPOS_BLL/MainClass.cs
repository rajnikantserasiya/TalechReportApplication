using FetchDataFromTalechPOS_BLL.EmployeeDetailsClasses;
using FetchDataFromTalechPOS_BLL.MerchantClasses;
using FetchDataFromTalechPOS_BLL.OrdersDetailsClasses;
using FetchDataFromTalechPOS_BLL.TokenGeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class MainClass
    {
        public async Task RunAsync()
        {
            //string startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 25)).ToString("MM/dd/yyyy HH:mm:ss");
            //string enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 26)).ToString("MM/dd/yyyy HH:mm:ss");

            try
            {
                TokenDetails objTokenDetails = new TokenDetails();
                MerchantDetails objMerchantDetails = new MerchantDetails();
                Orders objOrders = new Orders();
                MenuItems objMenuItems = new MenuItems();
                EmployeeDetails objEmployeeDetails = new EmployeeDetails();

                AuthenticationMethodResultModel result = await objTokenDetails.GetTalechAPI_Token();
                LogHelper.Log("Access token:" + result.access_token + " Expires in:" + result.expires_in + " token_type:" + result.token_type);

                Token.securityToken = result.access_token;

                List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation = await objMerchantDetails.GetAllMerchantStoreDetails();
                LogHelper.Log("Merchant Store Count: " + lstAllMerchantStoreInformation.Count() + " Time: " + DateTime.Now);

                await objMenuItems.GetMenuItemsByCriteria(lstAllMerchantStoreInformation);
                //await objMenuItems.GetMenuUpdatesByCriteria(lstAllMerchantStoreInformation);
                //LogHelper.Log("Menu Item and Category method completed. Count: " + lstAllMenuResultModel.Count() + " Time: " + DateTime.Now);

                List<MerchantIdentification_StoreName> lstAllMerchantStoreInformationawait = await objEmployeeDetails.GetEmployeeByCriteria(lstAllMerchantStoreInformation);
                LogHelper.Log("Get Employee list method completed. Time: " + DateTime.Now);

                //tustin
                //irvine
                //euclid
                //huntington beach
                //cypress,fountain valley,alhambra,artesia,chino hills,westminster,tustin,irvine,euclid,huntington beach,costa mesa                
                lstAllMerchantStoreInformation = lstAllMerchantStoreInformation.
                    Where(s => s.merchantStoreName.ToLower() == "tustin").ToList();

                string startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2018, 01, 25)).ToString("MM/dd/yyyy HH:mm:ss");
                string enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2018, 01, 26)).ToString("MM/dd/yyyy HH:mm:ss");
                await objOrders.GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                //await GetOrderHistoryByCriteriaTest(lstAllMerchantStoreInformation, startdate, enddate);
                //await GetOrderHistoryByCriteria(lstAllMerchantStoreInformation, startdate, enddate);
                LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //await GetOrderDetailsByOrderID(lstAllMerchantStoreInformation);
                //await LogOff(lstAllMerchantStoreInformation);
                //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation,startdate,enddate);
                //LogHelper.Log("DownloadStoreRevenueReport method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                // Console.WriteLine("File saved successfully");
            }
            catch (Exception ex)
            {

                LogHelper.Log("Erro in Run Async method. Error: " + ex.Message);
            }

        }
    }
}
