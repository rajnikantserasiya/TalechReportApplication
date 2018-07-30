using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class TalechAPIMethods
    {

        public string securityToken = string.Empty;
        public int LoginUserID = 0;

        List<AllMenuResultModel> lstAllMenuResultModel = new List<AllMenuResultModel>();

        public async Task RunAsync()
        {
            //string startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 25)).ToString("MM/dd/yyyy HH:mm:ss");
            //string enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 26)).ToString("MM/dd/yyyy HH:mm:ss");

            //FileInfo objFile = GetFileInfo(AppDomain.CurrentDomain.BaseDirectory, "Summary_" + Convert.ToDateTime(GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 25))).ToString("MM-dd-yyyy") + "_" + Convert.ToDateTime(GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 25))).ToString("MM-dd-yyyy") + ".xlsx");

            try
            {
                AuthenticationMethodResultModel result = await GetTalechAPI_Token();
                LogHelper.Log("Access token:" + result.access_token + " Expires in:" + result.expires_in + " token_type:" + result.token_type);

                securityToken = result.access_token;

                List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation = await GetAllMerchantStoreDetails();
                LogHelper.Log("Merchant Store Count: " + lstAllMerchantStoreInformation.Count() + " Time: " + DateTime.Now);

                await GetMenuItemsByCriteria(lstAllMerchantStoreInformation);
                //await GetMenuUpdatesByCriteria(lstAllMerchantStoreInformation);
                LogHelper.Log("Menu Item and Category method completed. Count: " + lstAllMenuResultModel.Count() + " Time: " + DateTime.Now);

                //List<MerchantIdentification_StoreName> lstAllMerchantStoreInformationawait = await GetEmployeeByCriteria(lstAllMerchantStoreInformation);
                //LogHelper.Log("Get Employee list method completed. Time: " + DateTime.Now);

                //cypress,fountain valley,alhambra,artesia,chino hills,westminster,tustin,irvine,euclid,huntington beach,costa mesa                
                lstAllMerchantStoreInformation = lstAllMerchantStoreInformation.Where(s => s.merchantStoreName.ToLower() == "tustin").ToList();  


                int Year = 2017;
                int Month = 7;              
                string startdate = string.Empty;
                string enddate = string.Empty;                         

                startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(Year, Month, 20)).ToString("MM/dd/yyyy HH:mm:ss");
                enddate = GeneralHelper.ResetTimeToEndOfDay(new DateTime(Year, Month, 20)).ToString("MM/dd/yyyy HH:mm:ss");
                await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //int Year = 2017;
                //int Month = 8;
                //int StartDay = 01;
                //string startdate = string.Empty;
                //string enddate = string.Empty;

                //for (int i = StartDay; i < 32; i++)
                //{
                //    startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    enddate = GeneralHelper.ResetTimeToEndOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //    await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);
                //}


                //Month = 9;                             
                //for (int i = StartDay; i < 31; i++)
                //{
                //    startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    enddate = GeneralHelper.ResetTimeToEndOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //    await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);
                //}

                //Month = 10;
                //for (int i = StartDay; i < 32; i++)
                //{
                //    startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    enddate = GeneralHelper.ResetTimeToEndOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //    await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);
                //}

                //Month = 11;
                //for (int i = StartDay; i < 31; i++)
                //{
                //    startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    enddate = GeneralHelper.ResetTimeToEndOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //    await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);
                //}

                //Month = 12;
                //for (int i = StartDay; i < 32; i++)
                //{
                //    startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    enddate = GeneralHelper.ResetTimeToEndOfDay(new DateTime(Year, Month, i)).ToString("MM/dd/yyyy HH:mm:ss");
                //    await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                //    await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                //    LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);
                //}





                //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
                //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

                // Console.WriteLine("File saved successfully");
            }
            catch (Exception ex)
            {

                LogHelper.Log("Erro in Run Async method. Error: " + ex.Message);
            }

        }

        public async Task<int> GetOrderHistoryByCriteriaTestNew(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation, string startdate, string enddate)
        {

            List<OrderDetailsExportFields> lstFinalResult = new List<OrderDetailsExportFields>();
            LogHelper.Log("Start Date: " + startdate + " End Date: " + enddate);
            foreach (var objAllMerchantStoreInformation in lstAllMerchantStoreInformation)
            {

                OrderHistorySearchCriteria objReportSearchCriteria = new OrderHistorySearchCriteria();
                objReportSearchCriteria.searchCriteria = new OrderHistoryInputParametersModel();
                objReportSearchCriteria.searchCriteria =
                 new OrderHistoryInputParametersModel()
                 {
                     offset = 0,
                     limit = 100000,
                     //searchString = "",
                     startDate = startdate,
                     endDate = enddate,
                     //paymentTypes = new string[] { "CASH", "CCRD", "DCRD", "CHECK" },
                     orderStatus = new string[] { "PAID", "PARTIAL_PAID", "OPEN" },
                     transactionTypes = new string[] { "SALE", "VOID", "REFUND", "PARTIALREFUND", "PARTIALVOID", "UPDATETIP" },
                     storeIds = new int[] { 1 },
                     orderBy = "firstPaymentDate",
                     //userId = objEmployee.id,//232741//

                     //Sale option
                     hideSale = false,  // To display only refund transaction
                     hideRefund = true, // To display only sales transaction
                     onlySplitCheck = false // To display only split check transaction

                     //Refund option
                     //hideSale = true,  // To display only refund transaction
                     //hideRefund = false, // To display only sales transaction
                     //onlySplitCheck = false // To display only split check transaction

                     //Split Check option
                     //hideSale = false,  // To display only refund transaction
                     //hideRefund = false, // To display only sales transaction
                     //onlySplitCheck = true // To display only split check transaction
                 };

                string jsonString = JsonConvert.SerializeObject(objReportSearchCriteria);
                JObject objInputParameters = JObject.Parse(jsonString);

                try
                {
                    string objRes = string.Empty;
                    OrderHistoryFullObject objOrderHistoryFullObject = null;
                    using (HttpClient client = new HttpClient())
                    {
                        SetHTTPClientObjectValues(client);
                        HttpResponseMessage response = await ExecuteClientPostMethod("order/getorderhistory", objInputParameters, client, securityToken, objAllMerchantStoreInformation.merchantIdentification);
                        if (response.IsSuccessStatusCode)
                        {
                            SetRefreshToken(response);
                            objRes = await response.Content.ReadAsStringAsync();
                            objOrderHistoryFullObject = JsonConvert.DeserializeObject<OrderHistoryFullObject>(objRes);
                        }

                    }

                    //if (!string.IsNullOrEmpty(objRes) && !objRes.ToLower().Contains("records not found"))
                    if (objOrderHistoryFullObject != null && objOrderHistoryFullObject.ResponseCode.statusCode == 200)
                    {
                        try
                        {
                            JObject jsonRes = JObject.Parse(objRes);
                            JToken jTokenSearchResult = jsonRes.FindTokens("searchResult").FirstOrDefault();
                            JObject JobjSearchResult = JObject.Parse(jTokenSearchResult.ToString());
                            JToken jTokenOrder = jsonRes.FindTokens("orders").FirstOrDefault();

                            //LogHelper.Log("Employee: " + objEmployee.userName + " Emp ID: " + objEmployee.id + " Orders Count: " + jTokenOrder.Children().Count() + " Time: " + DateTime.Now);
                            LogHelper.Log("Store: " + objAllMerchantStoreInformation.merchantStoreName + " Orders Count: " + jTokenOrder.Children().Count() + " Time: " + DateTime.Now);

                            List<OrderDetailsExportFields> lstStoreResult = new List<OrderDetailsExportFields>();

                            foreach (var jObjectOrder in jTokenOrder.Children())
                            {
                                OrderHistoryResultModel objOrderHistory = JsonConvert.DeserializeObject<OrderHistoryResultModel>(jObjectOrder.ToString());

                                if (!lstStoreResult.Any(s => s.TicketNo == objOrderHistory.transactionCode && objOrderHistory.paymentType == "SPLITCHECK"))
                                {
                                    OrderDetailsExportFields objOrderDetailsExportFields = new OrderDetailsExportFields();
                                    objOrderDetailsExportFields.TicketNo = objOrderHistory.transactionCode;
                                    objOrderDetailsExportFields.Type = objOrderHistory.orderType;
                                    objOrderDetailsExportFields.Employee = objOrderHistory.userFirstName + " " + objOrderHistory.userLastName;
                                    objOrderDetailsExportFields.Store = objAllMerchantStoreInformation.merchantStoreName;
                                    objOrderDetailsExportFields.Date = Convert.ToDateTime(objOrderHistory.orderDate).ToString("MM/dd/yyyy");
                                    objOrderDetailsExportFields.Time = Convert.ToDateTime(objOrderHistory.orderDate).ToString("hh:mm tt");
                                    objOrderDetailsExportFields.ItemName = objOrderHistory.listOfItems;
                                    objOrderDetailsExportFields.PaymentType = objOrderHistory.paymentType;
                                    objOrderDetailsExportFields.TransactionType = objOrderHistory.paymentInfo.FirstOrDefault().transactionType;
                                    objOrderDetailsExportFields.GrossSale = objOrderHistory.subTotal;
                                    objOrderDetailsExportFields.Discounts = objOrderHistory.discount;
                                    objOrderDetailsExportFields.Refunds = objOrderHistory.refundAmount;
                                    objOrderDetailsExportFields.NetSale = objOrderDetailsExportFields.GrossSale - objOrderDetailsExportFields.Discounts - objOrderDetailsExportFields.Refunds;
                                    objOrderDetailsExportFields.Tips = objOrderHistory.tip;
                                    objOrderDetailsExportFields.Tax = objOrderHistory.tax;
                                    objOrderDetailsExportFields.Total = objOrderDetailsExportFields.NetSale + objOrderDetailsExportFields.Tax + objOrderDetailsExportFields.Tips;

                                    //lstStoreResult.Add(objOrderDetailsExportFields);

                                    //OrderHistoryResultModel objOrderHistory = JsonConvert.DeserializeObject<OrderHistoryResultModel>(jObjectOrder.ToString());
                                    if (objOrderHistory != null && !string.IsNullOrEmpty(objOrderHistory.orderId))
                                    {
                                        List<OrderDetailsExportFields> objResult =
                                            await GetOrderDetailsByOrderID(objOrderHistory.orderId, objOrderHistory.transactionCode, objAllMerchantStoreInformation.merchantIdentification, objOrderHistory.userFirstName + " " + objOrderHistory.userLastName, objAllMerchantStoreInformation.merchantStoreName);

                                        if (objResult != null && objResult.Count() > 0)
                                        {
                                            lstStoreResult.AddRange(objResult);

                                            double ItemDetailsSum = objResult.Sum(s => s.NetSale);
                                            //double difference = objOrderDetailsExportFields.NetSale - ItemDetailsSum;
                                            //if (!objOrderDetailsExportFields.NetSale.Equals3DigitPrecision(ItemDetailsSum))
                                            //{
                                            if (Math.Abs(objOrderDetailsExportFields.NetSale - ItemDetailsSum) > 0.1)
                                            {
                                                double difference = objOrderDetailsExportFields.NetSale - ItemDetailsSum;
                                                LogHelper.Log("Ticket no: " + objOrderHistory.transactionCode + " Order ID:" + objOrderHistory.orderId + " Order NetSale: " + objOrderDetailsExportFields.NetSale + " Order Details Net Sale: " + ItemDetailsSum + " Difference: " + difference + " Order Date:" + objOrderDetailsExportFields.Date + " Time:" + objOrderDetailsExportFields.Time);
                                            }

                                            double GrossSaleSum = objResult.Sum(s => s.GrossSale);
                                            //double GrossSaledifference = objOrderDetailsExportFields.GrossSale - GrossSaleSum;
                                            if (Math.Abs(objOrderDetailsExportFields.GrossSale - GrossSaleSum) > 0.1)
                                            {
                                                double GrossSaledifference = objOrderDetailsExportFields.GrossSale - GrossSaleSum;
                                                LogHelper.Log("Ticket no: " + objOrderHistory.transactionCode + " Order ID:" + objOrderHistory.orderId + " Order GrossSale: " + objOrderDetailsExportFields.GrossSale + " Order Details Gross Sale: " + GrossSaleSum + " Difference: " + GrossSaledifference + " Order Date:" + objOrderDetailsExportFields.Date + " Time:" + objOrderDetailsExportFields.Time);
                                            }
                                        }
                                    }


                                }
                                else
                                {
                                    LogHelper.Log("Transaction is duplicate. Transaction Code: " + objOrderHistory.transactionCode);
                                }
                            }

                            lstFinalResult.AddRange(lstStoreResult);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Log("Error: " + ex.Message);
                        }

                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log("Error: " + ex.Message);
                }

            }

            try
            {

                LogHelper.Log("Total Records: " + lstFinalResult.Count());
                //lstFinalResult = lstFinalResult.OrderBy(s => s.TicketNo).ToList();
                SaveOrderDetailsIntoExcel(lstFinalResult, "ItemDetails_All_Stores_" + Convert.ToDateTime(startdate).ToString("MM-dd-yyyy"));
                LogHelper.Log("File saved successfully. " + DateTime.Now);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Error in save data into excel. " + ex.Message);
            }

            return 0;

        }

        public async Task<List<OrderDetailsExportFields>> GetOrderDetailsByOrderID(string orderID, string transactionCode, string merchantIdentification, string Employeename, string storeName)
        {
            List<OrderDetailsExportFields> lstOrderDetailsExportFields = new List<OrderDetailsExportFields>();
            try
            {

                OrderDetails objOrderDetails = new OrderDetails()
                {
                    orderId = orderID//"10075532813763"
                };

                string jsonString = JsonConvert.SerializeObject(objOrderDetails);
                JObject objInputParameters = JObject.Parse(jsonString);

                using (HttpClient client = new HttpClient())
                {
                    SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await ExecuteClientPostMethod("order/getorderbyid", objInputParameters, client, securityToken, merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {
                        SetRefreshToken(response);

                        var objRes = await response.Content.ReadAsStringAsync();
                        OrderDetailsResultModel objOrderDetailsResultModel = JsonConvert.DeserializeObject<OrderDetailsResultModel>(objRes.ToString());

                        double tips = 0.0;
                        foreach (OrderDetail objOrderDetail in objOrderDetailsResultModel.Order.orderDetails)
                        {
                            OrderDetailsExportFields objOrderDetailsExportFields = setOrderObject(objOrderDetailsResultModel, objOrderDetail, transactionCode, Employeename, storeName, ref tips);
                            lstOrderDetailsExportFields.Add(objOrderDetailsExportFields);

                            if (objOrderDetail.addOns != null && objOrderDetail.addOns.Count() > 0)
                            {
                                foreach (AddOns objAddOns in objOrderDetail.addOns)
                                {
                                    OrderDetailsExportFields objOrderDetailsExportFields_AddOns = new OrderDetailsExportFields();
                                    objOrderDetailsExportFields_AddOns.TicketNo = transactionCode;
                                    objOrderDetailsExportFields_AddOns.Type = objOrderDetailsResultModel.Order.orderType;
                                    objOrderDetailsExportFields_AddOns.Employee = Employeename;
                                    objOrderDetailsExportFields_AddOns.Store = storeName;
                                    objOrderDetailsExportFields_AddOns.Date = Convert.ToDateTime(objOrderDetailsResultModel.Order.orderCreationTime).ToString("MM/dd/yyyy");
                                    objOrderDetailsExportFields_AddOns.Time = Convert.ToDateTime(objOrderDetailsResultModel.Order.orderCreationTime).ToString("hh:mm tt");
                                    var categoryObj = lstAllMenuResultModel.FirstOrDefault(s => s.name.Contains(objAddOns.name));
                                    if (categoryObj != null)
                                        objOrderDetailsExportFields_AddOns.CategoryName = lstAllMenuResultModel.FirstOrDefault(s => s.name.Contains(objAddOns.name)).categoryType;//objCategory.FirstOrDefault(s => s.Key == objOrderDetail.categoryId.ToString()).Value;
                                    objOrderDetailsExportFields_AddOns.ItemName = objAddOns.name;
                                    if (objAddOns.refundedQuantity == 0.0 && objAddOns.quantity < objAddOns.printedQuantity && !objAddOns.quantity.Equals3DigitPrecision(objAddOns.printedQuantity))
                                    {
                                        objOrderDetailsExportFields_AddOns.QtySold = objAddOns.printedQuantity;
                                        LogHelper.Log("AddOns Item: Quantity: " + objAddOns.quantity + ",Printed Quantity: " + objAddOns.printedQuantity + ",Refunded Quantity: " + objAddOns.refundedQuantity);
                                    }
                                    else
                                        objOrderDetailsExportFields_AddOns.QtySold = objAddOns.quantity + objAddOns.refundedQuantity;

                                    objOrderDetailsExportFields_AddOns.PaymentType = objOrderDetailsResultModel.Order.paymentDetails.FirstOrDefault().paymentType;
                                    objOrderDetailsExportFields_AddOns.CreditType = objOrderDetailsResultModel.Order.paymentDetails.FirstOrDefault().cardType;
                                    objOrderDetailsExportFields_AddOns.GrossSale = objAddOns.price * objOrderDetailsExportFields_AddOns.QtySold;
                                    objOrderDetailsExportFields_AddOns.Discounts = objAddOns.discountAmt * (objAddOns.quantity + objAddOns.refundedQuantity);
                                    objOrderDetailsExportFields_AddOns.Refunds = objAddOns.price * objAddOns.refundedQuantity;
                                    objOrderDetailsExportFields_AddOns.NetSale =
                                        (objOrderDetailsExportFields_AddOns.GrossSale - objOrderDetailsExportFields_AddOns.Discounts - objOrderDetailsExportFields_AddOns.Refunds) < 0 ? 0 : (objOrderDetailsExportFields_AddOns.GrossSale - objOrderDetailsExportFields_AddOns.Discounts - objOrderDetailsExportFields_AddOns.Refunds);
                                    objOrderDetailsExportFields_AddOns.Tips = 0.0;
                                    objOrderDetailsExportFields_AddOns.Tax = objAddOns.tax;
                                    objOrderDetailsExportFields_AddOns.Total = objOrderDetailsExportFields_AddOns.NetSale + objOrderDetailsExportFields_AddOns.Tax + objOrderDetailsExportFields_AddOns.Tips;

                                    lstOrderDetailsExportFields.Add(objOrderDetailsExportFields_AddOns);
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("Error in Order details method. OrderID: " + orderID + " Error: " + ex.Message);
                //throw;
            }

            return lstOrderDetailsExportFields;

        }

        public OrderDetailsExportFields setOrderObject(OrderDetailsResultModel objOrderDetailsResultModel, OrderDetail objOrderDetail,
            string transactionCode, string Employeename, string storeName, ref double tips)
        {

            OrderDetailsExportFields objOrderDetailsExportFields = new OrderDetailsExportFields();
            objOrderDetailsExportFields.TicketNo = transactionCode;
            objOrderDetailsExportFields.Type = objOrderDetailsResultModel.Order.orderType;
            objOrderDetailsExportFields.Employee = Employeename;
            objOrderDetailsExportFields.Store = storeName;
            objOrderDetailsExportFields.Date = Convert.ToDateTime(objOrderDetailsResultModel.Order.orderCreationTime).ToString("MM/dd/yyyy");
            objOrderDetailsExportFields.Time = Convert.ToDateTime(objOrderDetailsResultModel.Order.orderCreationTime).ToString("hh:mm tt");
            var categoryObj = lstAllMenuResultModel.FirstOrDefault(s => s.name.Contains(objOrderDetail.name));
            if (categoryObj != null)
                objOrderDetailsExportFields.CategoryName = lstAllMenuResultModel.FirstOrDefault(s => s.name.Contains(objOrderDetail.name)).categoryType;//objCategory.FirstOrDefault(s => s.Key == objOrderDetail.categoryId.ToString()).Value;
            objOrderDetailsExportFields.ItemName = objOrderDetail.name;
            //LogHelper.Log("Regular Item: Quantity: " + objOrderDetail.quantity + ",Printed Quantity: " + objOrderDetail.printedQuantity + ",Refunded Quantity: " + objOrderDetail.refundedQuantity);
            objOrderDetailsExportFields.QtySold = objOrderDetail.quantity + objOrderDetail.refundedQuantity;
            objOrderDetailsExportFields.PaymentType = objOrderDetailsResultModel.Order.paymentDetails.FirstOrDefault().paymentType;
            objOrderDetailsExportFields.CreditType = objOrderDetailsResultModel.Order.paymentDetails.FirstOrDefault().cardType;
            objOrderDetailsExportFields.GrossSale = objOrderDetail.price * objOrderDetailsExportFields.QtySold;
            objOrderDetailsExportFields.Discounts = objOrderDetail.discountAmt * objOrderDetailsExportFields.QtySold;
            objOrderDetailsExportFields.Refunds = objOrderDetail.price * objOrderDetail.refundedQuantity;
            //If net sale is less than 0 then all items are refunded. So net sale = 0
            objOrderDetailsExportFields.NetSale =
                (objOrderDetailsExportFields.GrossSale - objOrderDetailsExportFields.Discounts - objOrderDetailsExportFields.Refunds) < 0 ? 0 : (objOrderDetailsExportFields.GrossSale - objOrderDetailsExportFields.Discounts - objOrderDetailsExportFields.Refunds);

            if (tips == 0.0)
                tips = objOrderDetailsExportFields.Tips = objOrderDetailsResultModel.Order.tipsAmount;
            else
                objOrderDetailsExportFields.Tips = 0.0;
            objOrderDetailsExportFields.Tax = objOrderDetail.tax;
            objOrderDetailsExportFields.Total = objOrderDetailsExportFields.NetSale + objOrderDetailsExportFields.Tax + objOrderDetailsExportFields.Tips;

            return objOrderDetailsExportFields;
        }

        public async Task<int> GetMenuItemsByCriteria(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation)
        {

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
                    SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await ExecuteClientPostMethod("managemenu/menuitem/allmenuitems", objInputParameters, client, securityToken, obj.merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {

                        SetRefreshToken(response);
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

            DataTable dt = lstAllMenuResultModel.ToDataTable();
            return 0;

        }

        public async Task<int> GetMenuUpdatesByCriteria(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation)
        {

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
                    SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await ExecuteClientPostMethod("managemenu/getMenuUpdates", objInputParameters, client, securityToken, obj.merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {

                        SetRefreshToken(response);
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

        public void SaveOrderDetailsIntoExcel(List<OrderDetailsExportFields> lstFinalResult, string sheetName)
        {

            if (lstFinalResult != null && lstFinalResult.Count() > 0)
            {
                DataTable dtOrderDetail = new DataTable("Order_Details");
                dtOrderDetail.Columns.Add("Ticket #");
                dtOrderDetail.Columns.Add("Type", typeof(string));
                dtOrderDetail.Columns.Add("Employee", typeof(string));
                dtOrderDetail.Columns.Add("Date", typeof(string));
                dtOrderDetail.Columns.Add("Time", typeof(string));
                dtOrderDetail.Columns.Add("Store", typeof(string));
                dtOrderDetail.Columns.Add("Category", typeof(string));
                dtOrderDetail.Columns.Add("Item Name", typeof(string));
                dtOrderDetail.Columns.Add("Qty Sold", typeof(double));
                //dtOrderDetail.Columns.Add("Transaction Type", typeof(string));
                dtOrderDetail.Columns.Add("Payment Type", typeof(string));
                dtOrderDetail.Columns.Add("Credit Type", typeof(string));
                dtOrderDetail.Columns.Add("Gross Sale", typeof(double));
                dtOrderDetail.Columns.Add("Discounts", typeof(double));
                dtOrderDetail.Columns.Add("Refund", typeof(double));
                dtOrderDetail.Columns.Add("Net Sale", typeof(double));
                dtOrderDetail.Columns.Add("Tips", typeof(double));
                dtOrderDetail.Columns.Add("Tax", typeof(double));
                dtOrderDetail.Columns.Add("Total", typeof(double));


                foreach (var objResult in lstFinalResult)
                {
                    DataRow dr = dtOrderDetail.NewRow();

                    dr["Ticket #"] = objResult.TicketNo;
                    dr["Type"] = objResult.Type == "togo" ? "To Go" : "To Stay";
                    dr["Employee"] = objResult.Employee;
                    dr["Date"] = objResult.Date;
                    dr["Time"] = objResult.Time;
                    dr["Store"] = objResult.Store;
                    dr["Category"] = objResult.CategoryName;
                    dr["Item Name"] = objResult.ItemName;
                    dr["Qty Sold"] = objResult.QtySold;
                    //dr["Transaction Type"] = objResult.TransactionType;
                    dr["Payment Type"] = objResult.PaymentType;
                    dr["Credit Type"] = objResult.CreditType;
                    dr["Gross Sale"] = objResult.GrossSale;
                    dr["Discounts"] = objResult.Discounts;
                    dr["Refund"] = objResult.Refunds;
                    dr["Net Sale"] = objResult.NetSale;
                    dr["Tips"] = objResult.Tips;
                    dr["Tax"] = objResult.Tax;
                    dr["Total"] = objResult.Total;

                    dtOrderDetail.Rows.Add(dr);
                }

                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);

                    worksheet.Cells["A1"].LoadFromDataTable(dtOrderDetail, true, TableStyles.Light9);
                    worksheet.Cells.AutoFitColumns(0);
                    worksheet.Cells[2, 11, dtOrderDetail.Rows.Count + 1, 18].Style.Numberformat.Format = "$#,##0.00";

                    FileInfo objFile = GetFileInfo(AppDomain.CurrentDomain.BaseDirectory, sheetName + ".xlsx");
                    package.SaveAs(objFile);
                }
            }
        }

        public async Task<List<MerchantIdentification_StoreName>> GetEmployeeByCriteria(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation)
        {
            string startdate = DateTime.Now.Date.AddDays(-1).ToString("MM/dd/yyyy hh:mm:ss");
            string enddate = DateTime.Now.Date.ToString("MM/dd/yyyy hh:mm:ss");
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
                    SetHTTPClientObjectValues(client);
                    HttpResponseMessage response = await ExecuteClientPostMethod("manageemployee/getallemployees", objInputParameters, client, securityToken, obj.merchantIdentification);
                    if (response.IsSuccessStatusCode)
                    {
                        SetRefreshToken(response);
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

        public async Task<int> DownloadStoreRevenueReport(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation, string startdate, string enddate)
        {
            //string startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2018, 01, 25)).ToString("MM/dd/yyyy HH:mm:ss");
            //string enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2018, 01, 26)).ToString("MM/dd/yyyy HH:mm:ss");
            try
            {
                ReportSearchCriteria objReportSearchCriteria = new ReportSearchCriteria();
                objReportSearchCriteria.searchCriteria = new ReceiptSummaryReportInputParameterModel();
                objReportSearchCriteria.searchCriteria =
                 new ReceiptSummaryReportInputParameterModel()
                 {
                     //userID = 156638,
                     startDate = startdate,
                     endDate = enddate,
                     granularity = 4, //1 Daily, 2 Hourly, 4 - Monthly
                     includeShiftsData = false,
                     includedReports = new int[] { 103, 101 }
                 };

                string jsonString = JsonConvert.SerializeObject(objReportSearchCriteria);
                JObject objInputParameters = JObject.Parse(jsonString);

                //Merchant IDs: 304267,864093,782465,401041,706913,938269,146195,184349,260068,225657,322240
                //UserID : 34135

                List<SummaryResultModel> lstStoreSummaryAllResultModel = new List<SummaryResultModel>();
                List<StoreHourlySummaryResultModel> lstHourlySummary = new List<StoreHourlySummaryResultModel>();

                foreach (var obj in lstAllMerchantStoreInformation)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        SetHTTPClientObjectValues(client);
                        HttpResponseMessage response = await ExecuteClientPostMethod("reports/receiptssummaryreport", objInputParameters, client, securityToken, obj.merchantIdentification); //obj.merchantIdentification);
                        if (response.IsSuccessStatusCode)
                        {
                            SetRefreshToken(response);

                            var objRes = await response.Content.ReadAsStringAsync();
                            JObject jsonRes = JObject.Parse(objRes);

                            #region Hourly summary Json parse code

                            //JToken jTokenDailySummary = jsonRes.FindTokens("dailySummary").FirstOrDefault();
                            //JObject JobjDailySummary = JObject.Parse(jTokenDailySummary.ToString());
                            //foreach (var jObject in JobjDailySummary)
                            //{

                            //    JToken jTokenTimePeriodSummary = jObject.Value.FindTokens("timePeriodSummary").FirstOrDefault();
                            //    JToken jTokenAll = jTokenTimePeriodSummary.FindTokens("all").FirstOrDefault();

                            //    StoreHourlySummaryResultModel objStoreHourlySummaryResultModel = JsonConvert.DeserializeObject<StoreHourlySummaryResultModel>(jTokenAll.ToString());
                            //    objStoreHourlySummaryResultModel.storeName = obj.merchantStoreName;
                            //    objStoreHourlySummaryResultModel.DateTimestring = jObject.Key;
                            //    lstHourlySummary.Add(objStoreHourlySummaryResultModel);
                            //}

                            #endregion


                            #region Summary details Json parse code

                            JToken jToken = jsonRes.FindTokens("all").FirstOrDefault();

                            if (jToken != null)
                            {
                                SummaryResultModel objStoreSummaryAllResultModel = JsonConvert.DeserializeObject<SummaryResultModel>(jToken.ToString());

                                objStoreSummaryAllResultModel.storeName = obj.merchantStoreName;
                                lstStoreSummaryAllResultModel.Add(objStoreSummaryAllResultModel);
                            }
                            #endregion

                        }

                    }

                }

                //SaveHourlyNetRevenueDetailsIntoExcel(lstHourlySummary);

                //SaveHourlyRevenuePerReceiptIntoExcel(lstHourlySummary);

                //SaveHourlyReceiptIntoExcel(lstHourlySummary);

                SaveSummaryDetailsintoExcel(lstStoreSummaryAllResultModel, startdate, enddate);
            }
            catch (Exception ex)
            {

                LogHelper.Log("Error in Store Revenue report. Error: " + ex.Message);
            }

            return 0;

        }

        public void SaveHourlyNetRevenueDetailsIntoExcel(List<StoreHourlySummaryResultModel> lstHourlySummary)
        {

            if (lstHourlySummary != null && lstHourlySummary.Count() > 0)
            {
                DataTable dtStoreHourlySummary = new DataTable("Net_Revenue_Hourly_Summary");
                dtStoreHourlySummary.Columns.Add("Date");
                dtStoreHourlySummary.Columns.Add("Cypress", typeof(double));
                dtStoreHourlySummary.Columns.Add("Fountain Valley", typeof(double));
                dtStoreHourlySummary.Columns.Add("Alhambra", typeof(double));
                dtStoreHourlySummary.Columns.Add("Artesia", typeof(double));
                dtStoreHourlySummary.Columns.Add("Chino Hills", typeof(double));
                dtStoreHourlySummary.Columns.Add("Westminster", typeof(double));
                dtStoreHourlySummary.Columns.Add("Tustin", typeof(double));
                dtStoreHourlySummary.Columns.Add("Irvine", typeof(double));
                dtStoreHourlySummary.Columns.Add("Euclid", typeof(double));
                dtStoreHourlySummary.Columns.Add("Huntington Beach", typeof(double));
                dtStoreHourlySummary.Columns.Add("Costa Mesa", typeof(double));

                var lstDistinctDateTime = lstHourlySummary.GroupBy(s => s.DateTimestring).Select(s => new { s.Key }).ToList();

                foreach (var objDateTime in lstDistinctDateTime)
                {
                    DataRow dr = dtStoreHourlySummary.NewRow();

                    dr["Date"] = objDateTime.Key;
                    dr["Cypress"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "cypress").netRevenue;
                    dr["Fountain Valley"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "fountain valley").netRevenue;
                    dr["Alhambra"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "alhambra").netRevenue;
                    dr["Artesia"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "artesia").netRevenue;
                    dr["Chino Hills"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "chino hills").netRevenue;
                    dr["Westminster"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "westminster").netRevenue;
                    dr["Tustin"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "tustin").netRevenue;
                    dr["Irvine"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "irvine").netRevenue;
                    dr["Euclid"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "euclid").netRevenue;
                    dr["Huntington Beach"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "huntington beach").netRevenue;
                    dr["Costa Mesa"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "costa mesa").netRevenue;

                    dtStoreHourlySummary.Rows.Add(dr);
                }

                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Store Hourly Summary");
                    worksheet.Cells["A2"].Value = "Net Revenue";
                    using (var range = worksheet.Cells["A2"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 14;
                        range.Style.Font.Color.SetColor(Color.FromArgb(41, 128, 185));
                    }
                    worksheet.Cells["A3"].LoadFromDataTable(dtStoreHourlySummary, true, TableStyles.Light9);
                    worksheet.Cells.AutoFitColumns(0);
                    worksheet.Cells[3, 2, 27, 12].Style.Numberformat.Format = "$#,##0.00";

                    FileInfo objFile = GetFileInfo(@"C:\Users\Rajni\Desktop\E\Projects\Custom Data Reports via API\Excel Export", dtStoreHourlySummary.TableName + ".xlsx");
                    package.SaveAs(objFile);
                }
            }
        }

        public void SaveHourlyRevenuePerReceiptIntoExcel(List<StoreHourlySummaryResultModel> lstHourlySummary)
        {

            if (lstHourlySummary != null && lstHourlySummary.Count() > 0)
            {
                DataTable dtStoreHourlySummary = new DataTable("RevenuePerReceiptSummary");
                dtStoreHourlySummary.Columns.Add("Date");
                dtStoreHourlySummary.Columns.Add("Cypress", typeof(double));
                dtStoreHourlySummary.Columns.Add("Fountain Valley", typeof(double));
                dtStoreHourlySummary.Columns.Add("Alhambra", typeof(double));
                dtStoreHourlySummary.Columns.Add("Artesia", typeof(double));
                dtStoreHourlySummary.Columns.Add("Chino Hills", typeof(double));
                dtStoreHourlySummary.Columns.Add("Westminster", typeof(double));
                dtStoreHourlySummary.Columns.Add("Tustin", typeof(double));
                dtStoreHourlySummary.Columns.Add("Irvine", typeof(double));
                dtStoreHourlySummary.Columns.Add("Euclid", typeof(double));
                dtStoreHourlySummary.Columns.Add("Huntington Beach", typeof(double));
                dtStoreHourlySummary.Columns.Add("Costa Mesa", typeof(double));

                var lstDistinctDateTime = lstHourlySummary.GroupBy(s => s.DateTimestring).Select(s => new { s.Key }).ToList();

                foreach (var objDateTime in lstDistinctDateTime)
                {
                    DataRow dr = dtStoreHourlySummary.NewRow();

                    dr["Date"] = objDateTime.Key;
                    dr["Cypress"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "cypress").netRevenuePerReceipt;
                    dr["Fountain Valley"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "fountain valley").netRevenuePerReceipt;
                    dr["Alhambra"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "alhambra").netRevenuePerReceipt;
                    dr["Artesia"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "artesia").netRevenuePerReceipt;
                    dr["Chino Hills"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "chino hills").netRevenuePerReceipt;
                    dr["Westminster"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "westminster").netRevenuePerReceipt;
                    dr["Tustin"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "tustin").netRevenuePerReceipt;
                    dr["Irvine"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "irvine").netRevenuePerReceipt;
                    dr["Euclid"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "euclid").netRevenuePerReceipt;
                    dr["Huntington Beach"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "huntington beach").netRevenuePerReceipt;
                    dr["Costa Mesa"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "costa mesa").netRevenuePerReceipt;

                    dtStoreHourlySummary.Rows.Add(dr);
                }

                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hourly Revenue per Receipt");
                    worksheet.Cells["A2"].Value = "Revenue Per Receipt";
                    using (var range = worksheet.Cells["A2"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 14;
                        range.Style.Font.Color.SetColor(Color.FromArgb(41, 128, 185));
                    }
                    worksheet.Cells["A3"].LoadFromDataTable(dtStoreHourlySummary, true, TableStyles.Light9);
                    worksheet.Cells.AutoFitColumns(0);
                    worksheet.Cells[3, 2, 27, 12].Style.Numberformat.Format = "$#,##0.00";

                    FileInfo objFile = GetFileInfo(@"C:\Users\Rajni\Desktop\E\Projects\Custom Data Reports via API\Excel Export", dtStoreHourlySummary.TableName + ".xlsx");
                    package.SaveAs(objFile);
                }
            }
        }

        public void SaveHourlyReceiptIntoExcel(List<StoreHourlySummaryResultModel> lstHourlySummary)
        {

            if (lstHourlySummary != null && lstHourlySummary.Count() > 0)
            {
                DataTable dtStoreHourlySummary = new DataTable("HourlyReceipts");
                dtStoreHourlySummary.Columns.Add("Date");
                dtStoreHourlySummary.Columns.Add("Cypress", typeof(double));
                dtStoreHourlySummary.Columns.Add("Fountain Valley", typeof(double));
                dtStoreHourlySummary.Columns.Add("Alhambra", typeof(double));
                dtStoreHourlySummary.Columns.Add("Artesia", typeof(double));
                dtStoreHourlySummary.Columns.Add("Chino Hills", typeof(double));
                dtStoreHourlySummary.Columns.Add("Westminster", typeof(double));
                dtStoreHourlySummary.Columns.Add("Tustin", typeof(double));
                dtStoreHourlySummary.Columns.Add("Irvine", typeof(double));
                dtStoreHourlySummary.Columns.Add("Euclid", typeof(double));
                dtStoreHourlySummary.Columns.Add("Huntington Beach", typeof(double));
                dtStoreHourlySummary.Columns.Add("Costa Mesa", typeof(double));

                var lstDistinctDateTime = lstHourlySummary.GroupBy(s => s.DateTimestring).Select(s => new { s.Key }).ToList();

                foreach (var objDateTime in lstDistinctDateTime)
                {
                    DataRow dr = dtStoreHourlySummary.NewRow();

                    dr["Date"] = objDateTime.Key;
                    dr["Cypress"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "cypress").receipts;
                    dr["Fountain Valley"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "fountain valley").receipts;
                    dr["Alhambra"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "alhambra").receipts;
                    dr["Artesia"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "artesia").receipts;
                    dr["Chino Hills"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "chino hills").receipts;
                    dr["Westminster"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "westminster").receipts;
                    dr["Tustin"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "tustin").receipts;
                    dr["Irvine"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "irvine").receipts;
                    dr["Euclid"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "euclid").receipts;
                    dr["Huntington Beach"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "huntington beach").receipts;
                    dr["Costa Mesa"] = lstHourlySummary.FirstOrDefault(s => s.DateTimestring == objDateTime.Key && s.storeName.ToLower() == "costa mesa").receipts;

                    dtStoreHourlySummary.Rows.Add(dr);
                }

                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hourly Receipts");
                    worksheet.Cells["A2"].Value = "Receipts";
                    using (var range = worksheet.Cells["A2"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 14;
                        range.Style.Font.Color.SetColor(Color.FromArgb(41, 128, 185));
                    }
                    worksheet.Cells["A3"].LoadFromDataTable(dtStoreHourlySummary, true, TableStyles.Light9);
                    worksheet.Cells.AutoFitColumns(0);
                    //worksheet.Cells[3, 2, 27, 12].Style.Numberformat.Format = "$#,##0.00";

                    FileInfo objFile = GetFileInfo(@"C:\Users\Rajni\Desktop\E\Projects\Custom Data Reports via API\Excel Export", dtStoreHourlySummary.TableName + ".xlsx");
                    package.SaveAs(objFile);
                }
            }
        }

        public void SaveSummaryDetailsintoExcel(List<SummaryResultModel> lstStoreSummaryAllResultModel, string startDate, string endDate)
        {
            if (lstStoreSummaryAllResultModel != null && lstStoreSummaryAllResultModel.Count() > 0)
            {
                DataTable dtStoreSummary = new DataTable("Summary");
                dtStoreSummary.Columns.Add("Location");
                dtStoreSummary.Columns.Add("Receipts", typeof(int));
                dtStoreSummary.Columns.Add("Refund Receipts", typeof(int));
                dtStoreSummary.Columns.Add("Gross Revenue", typeof(double));
                dtStoreSummary.Columns.Add("Discounts", typeof(double));
                //dtStoreSummary.Columns.Add("Loyalty", typeof(int));
                dtStoreSummary.Columns.Add("Refunds", typeof(double));
                dtStoreSummary.Columns.Add("Net Revenue", typeof(double));
                //dtStoreSummary.Columns.Add("Gratuity", typeof(double));
                dtStoreSummary.Columns.Add("Tax", typeof(double));
                dtStoreSummary.Columns.Add("Tip", typeof(double));
                //dtStoreSummary.Columns.Add("Gift Card", typeof(double));
                dtStoreSummary.Columns.Add("Net Collected", typeof(double));
                //dtStoreSummary.Columns.Add("Cost", typeof(double));
                dtStoreSummary.Columns.Add("Profitability", typeof(double));

                foreach (var objStoreSummary in lstStoreSummaryAllResultModel)
                {
                    DataRow dr = dtStoreSummary.NewRow();

                    dr["Location"] = objStoreSummary.storeName;
                    dr["Receipts"] = objStoreSummary.receipts;
                    dr["Refund Receipts"] = objStoreSummary.refundReceipts;
                    dr["Gross Revenue"] = objStoreSummary.grossRevenue;
                    dr["Discounts"] = objStoreSummary.discount;
                    // dr["Loyalty"] = objStoreSummary.loyalty;
                    dr["Refunds"] = objStoreSummary.refundAmount;
                    dr["Net Revenue"] = objStoreSummary.netRevenue;
                    // dr["Gratuity"] = objStoreSummary.gratuity;
                    dr["Tax"] = objStoreSummary.tax;
                    dr["Tip"] = objStoreSummary.tip;
                    // dr["Gift Card"] = objStoreSummary.netGiftCardsRevenue;
                    dr["Net Collected"] = objStoreSummary.totalCollected;
                    //dr["Cost"] = objStoreSummary.cost;
                    dr["Profitability"] = objStoreSummary.profitability;

                    dtStoreSummary.Rows.Add(dr);
                }

                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Merchant Store Details");
                    worksheet.Cells["A2"].Value = "Summary from " + Convert.ToDateTime(startDate).ToString("MM/dd/yyyy") + " to " + Convert.ToDateTime(endDate).ToString("MM/dd/yyyy");
                    using (var range = worksheet.Cells["A2"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 14;
                        range.Style.Font.Color.SetColor(Color.FromArgb(41, 128, 185));
                    }
                    worksheet.Cells["A3"].LoadFromDataTable(dtStoreSummary, true, TableStyles.Light9);
                    worksheet.Cells.AutoFitColumns(0);
                    worksheet.Cells[3, 4, 13, 11].Style.Numberformat.Format = "$#,##0.00";
                    worksheet.Cells["E4:E14"].Style.Font.Color.SetColor(Color.Red);
                    worksheet.Cells["F4:F14"].Style.Font.Color.SetColor(Color.Red);

                    FileInfo objFile = GetFileInfo(AppDomain.CurrentDomain.BaseDirectory, "Summary_" + Convert.ToDateTime(startDate).ToString("MM-dd-yyyy") + ".xlsx");
                    package.SaveAs(objFile);
                }
            }
        }

        public async Task<List<MerchantIdentification_StoreName>> GetAllMerchantStoreDetails()
        {
            List<MerchantIdentification_StoreName> lstMerchantdetailsObj = new List<MerchantIdentification_StoreName>();
            AllMerchantStoreInfoModel objResult = new AllMerchantStoreInfoModel();


            using (HttpClient client = new HttpClient())
            {
                SetHTTPClientObjectValues(client);
                HttpResponseMessage response = await ExecuteClientPostMethod("authentication/getAllMerchantStoreInfo", null, client, securityToken);
                if (response.IsSuccessStatusCode)
                {
                    SetRefreshToken(response);





                    var objRes = await response.Content.ReadAsStringAsync();
                    objResult = JsonConvert.DeserializeObject<AllMerchantStoreInfoModel>(await response.Content.ReadAsStringAsync());

                    if (objResult.ResponseCode.statusCode == 200 && objResult.merchantStoreDetails != null && objResult.merchantStoreDetails.merchantDetails != null)
                    {
                        //securityToken = objResult.merchantStoreDetails.securityToken;
                        LoginUserID = objResult.merchantStoreDetails.user.id;

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

                        //foreach (var objMerchantDetails in objResult.merchantStoreDetails.merchantDetails)
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

                        //    FileInfo objFile = GetFileInfo(AppDomain.CurrentDomain.BaseDirectory, dtMerchantDetails.TableName + ".xlsx");
                        //    package.SaveAs(objFile);
                        //} 
                        #endregion

                    }
                }
            }

            return lstMerchantdetailsObj;
        }

        async Task<AuthenticationMethodResultModel> GetTalechAPI_Token()
        {
            AuthenticationMethodResultModel obj = new AuthenticationMethodResultModel();

            TalechAccountDetails objdetails = new TalechAccountDetails
            {
                refresh_token = "191072574/5kHP9zQJ8mADp8oSG15bw6x2",
                client_secret = "49B9GDPU",
                client_id = "rserasiya@gmail.com",
                grant_type = "refresh_token"
            };

            string jsonString = JsonConvert.SerializeObject(objdetails);
            JObject objInputParameters = JObject.Parse(jsonString);


            using (HttpClient client = new HttpClient())
            {
                SetHTTPClientObjectValues(client);
                HttpResponseMessage response = await ExecuteClientPostMethod("o/oauth2/token", objInputParameters, client);

                LogHelper.Log("Refresh Token Method Response code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    obj = await response.Content.ReadAsAsync<AuthenticationMethodResultModel>();
                }
            }

            return obj;
        }

        bool SetRefreshToken(HttpResponseMessage response)
        {
            HttpHeaders headers = response.Headers;
            IEnumerable<string> values;
            if (headers.TryGetValues("X-POS-SecurityToken", out values))
            {
                securityToken = values.First();

                //Console.WriteLine("Security Token: " + securityToken);
            }

            return true;
        }

        async Task<HttpResponseMessage> ExecuteClientPostMethod(string uri, JObject inputParameters, HttpClient client, string securityToken = default(string),
            string merchantID = default(string))
        {

            if (!string.IsNullOrEmpty(securityToken))
                client.DefaultRequestHeaders.Add("securityToken", securityToken);
            if (!string.IsNullOrEmpty(merchantID))
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-POS-MerchantId", merchantID.ToString());

            HttpResponseMessage response = await client.PostAsJsonAsync(uri, inputParameters);

            return response;
        }

        public bool SetHTTPClientObjectValues(HttpClient client)
        {
            //client = new HttpClient();
            client.BaseAddress = new Uri("https://mapi.talech.com​/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return true;
        }

        public FileInfo GetFileInfo(string dirPath, string file, bool deleteIfExists = true)
        {
            dirPath = dirPath + "\\Excel Reports\\";
            var fi = new FileInfo(dirPath + Path.DirectorySeparatorChar + file);
            if (deleteIfExists && fi.Exists)
            {
                fi.Delete();  // ensures we create a new workbook
            }
            return fi;
        }

        public void DateCollection()
        {
            #region Date range
            //StartDay = EndDay; EndDay = EndDay + 1;
            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, Month, StartDay)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, Month, EndDay)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);


            //StartDay = EndDay; EndDay = EndDay + 1;
            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, Month, StartDay)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, Month, EndDay)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);


            //StartDay = EndDay; EndDay = EndDay + 1;
            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, Month, StartDay)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, Month, EndDay)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);


            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 05)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 06)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);


            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 06)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 07)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);


            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 07)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 08)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 08)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 09)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 09)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 10)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 10)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 11)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 11)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 12)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 12)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 13)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 13)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 14)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 14)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 15)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 15)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 16)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 16)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 17)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 17)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 18)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 18)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 19)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 19)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 20)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 20)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 21)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 21)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 22)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 22)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 23)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 23)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 24)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 24)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 25)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 25)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 26)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 26)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 27)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 27)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 28)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 28)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 29)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 29)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 30)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 30)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 31)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //startdate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 01, 31)).ToString("MM/dd/yyyy HH:mm:ss");
            //enddate = GeneralHelper.ResetTimeToStartOfDay(new DateTime(2017, 02, 01)).ToString("MM/dd/yyyy HH:mm:ss");
            //await GetOrderHistoryByCriteriaTestNew(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("GetOrderHistoryByCriteriaTestNew method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now);

            //await DownloadStoreRevenueReport(lstAllMerchantStoreInformation, startdate, enddate);
            //LogHelper.Log("Download Store Revenue Report method completed for Start Date: " + startdate + " End Date: " + enddate + " Time: " + DateTime.Now); 
            #endregion
        }
    }

}
