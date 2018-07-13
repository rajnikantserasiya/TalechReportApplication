using FetchDataFromTalechPOS_BLL.TokenGeneralClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL.MerchantClasses
{
    public class MerchantDetails
    {
        public async Task<List<MerchantIdentification_StoreName>> GetAllMerchantStoreDetails()
        {
            List<MerchantIdentification_StoreName> lstMerchantdetailsObj = new List<MerchantIdentification_StoreName>();
            AllMerchantStoreInfoModel objResult = new AllMerchantStoreInfoModel();
            TokenDetails objTokenDetails = new TokenDetails();

            using (HttpClient client = new HttpClient())
            {
                objTokenDetails.SetHTTPClientObjectValues(client);
                HttpResponseMessage response = await objTokenDetails.ExecuteClientPostMethod("authentication/getAllMerchantStoreInfo", null, client, Token.securityToken);
                if (response.IsSuccessStatusCode)
                {
                    objTokenDetails.SetRefreshToken(response);
                    var objRes = await response.Content.ReadAsStringAsync();
                    objResult = JsonConvert.DeserializeObject<AllMerchantStoreInfoModel>(await response.Content.ReadAsStringAsync());

                    if (objResult.ResponseCode.statusCode == 200 && objResult.merchantStoreDetails != null && objResult.merchantStoreDetails.merchantDetails != null)
                    {
                        //securityToken = objResult.merchantStoreDetails.securityToken;
                        //LoginUserID = objResult.merchantStoreDetails.user.id;

                        foreach (var objMerchantDetails in objResult.merchantStoreDetails.merchantDetails)
                        {
                            MerchantIdentification_StoreName obj = new MerchantIdentification_StoreName();
                            obj.merchantIdentification = objMerchantDetails.merchantIdentification.ToString();
                            obj.merchantStoreName = objMerchantDetails.stores.FirstOrDefault().storeName;
                            obj.storeId = objMerchantDetails.stores.FirstOrDefault().storeId;
                            obj.merchantId = objMerchantDetails.stores.FirstOrDefault().merchantId;

                            lstMerchantdetailsObj.Add(obj);
                        }

                        #region Download Excel for Merchant Store details
                        //DataTable dtMerchantDetails = new DataTable("Merchant_Store_Details");
                        //dtMerchantDetails.Columns.Add("Merchant ID", typeof(Int64));
                        //dtMerchantDetails.Columns.Add("Business Name");
                        //dtMerchantDetails.Columns.Add("Business Phone");
                        //dtMerchantDetails.Columns.Add("Address1");
                        //dtMerchantDetails.Columns.Add("Address2");
                        //dtMerchantDetails.Columns.Add("City");
                        //dtMerchantDetails.Columns.Add("State");
                        //dtMerchantDetails.Columns.Add("Country");
                        //dtMerchantDetails.Columns.Add("Zip");
                        //dtMerchantDetails.Columns.Add("Store name");
                        //dtMerchantDetails.Columns.Add("Currency");

                        //foreach (var objMerchantDetails in objresult.merchantStoreDetails.merchantDetails)
                        //{
                        //    DataRow dr = dtMerchantDetails.NewRow();

                        //    var storeInfo = objMerchantDetails.stores.FirstOrDefault();
                        //    dr["Merchant ID"] = Convert.ToInt64(objMerchantDetails.merchantIdentification);
                        //    dr["Business Name"] = storeInfo.businessName;
                        //    dr["Business Phone"] = storeInfo.businessPhone;
                        //    dr["Address1"] = storeInfo.address.address1;
                        //    dr["Address2"] = storeInfo.address.address2;
                        //    dr["City"] = storeInfo.address.city;
                        //    dr["State"] = storeInfo.address.state;
                        //    dr["Country"] = storeInfo.address.country;
                        //    dr["Zip"] = storeInfo.address.zip;
                        //    dr["Store name"] = storeInfo.storeName;
                        //    dr["Currency"] = storeInfo.currency;

                        //    dtMerchantDetails.Rows.Add(dr);
                        //}

                        //using (var package = new ExcelPackage())
                        //{
                        //    // Add a new worksheet to the empty workbook
                        //    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Merchant Store Details");
                        //    worksheet.Cells["A1"].LoadFromDataTable(dtMerchantDetails, true, TableStyles.Light9);
                        //    worksheet.Cells.AutoFitColumns(0);

                        //    FileInfo objFile = GetFileInfo(@"C:\Users\Rajni\Desktop\E\Projects\Custom Data Reports via API\Excel Export", dtMerchantDetails.TableName + ".xlsx");
                        //    package.SaveAs(objFile);
                        //} 
                        #endregion

                    }
                }
            }

            return lstMerchantdetailsObj;
        }
    }
}
