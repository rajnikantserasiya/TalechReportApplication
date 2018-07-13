using FetchDataFromTalechPOS_BLL.TokenGeneralClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL.OrdersDetailsClasses
{
    public class Orders
    {

        public async Task<int> GetOrderHistoryByCriteriaTestNew(List<MerchantIdentification_StoreName> lstAllMerchantStoreInformation, string startdate, string enddate)
        {
            TokenDetails objTokenDetails = new TokenDetails();
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
                     limit = 10000,
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
                     //hideRefund = true, // To display only sales transaction
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
                        objTokenDetails.SetHTTPClientObjectValues(client);
                        HttpResponseMessage response = await objTokenDetails.ExecuteClientPostMethod("order/getorderhistory", objInputParameters, client, Token.securityToken, objAllMerchantStoreInformation.merchantIdentification);
                        if (response.IsSuccessStatusCode)
                        {
                            objTokenDetails.SetRefreshToken(response);
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

                                if (!lstStoreResult.Any(s => s.TicketNo == objOrderHistory.transactionCode))
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

                                    lstStoreResult.Add(objOrderDetailsExportFields);
                                    //lstFinalResult.Add(objOrderDetailsExportFields);

                                    //OrderHistoryResultModel objOrderHistory = JsonConvert.DeserializeObject<OrderHistoryResultModel>(jObjectOrder.ToString());
                                    //if (objOrderHistory != null && !string.IsNullOrEmpty(objOrderHistory.orderId))
                                    //{
                                    //    List<OrderDetailsExportFields> objResult = await GetOrderDetailsByOrderID(objOrderHistory.orderId, objOrderHistory.transactionCode, objAllMerchantStoreInformation.merchantIdentification, objOrderHistory.userFirstName + " " + objOrderHistory.userLastName, objAllMerchantStoreInformation.merchantStoreName);

                                    //    if (objResult != null && objResult.Count() > 0)
                                    //        lstFinalResult.AddRange(objResult);
                                    //}
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
                SaveOrderDetailsIntoExcel(lstFinalResult, lstFinalResult.FirstOrDefault().Store + "_" + Convert.ToDateTime(startdate).ToString("MMM-yy"));
                LogHelper.Log("File saved successfully. " + DateTime.Now);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Error in save data into excel. " + ex.Message);
            }

            return 0;

        }

        public void SaveOrderDetailsIntoExcel(List<OrderDetailsExportFields> lstFinalResult, string sheetName)
        {
            TokenDetails objTokenDetails = new TokenDetails();
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
                dtOrderDetail.Columns.Add("Qty Sold", typeof(int));
                dtOrderDetail.Columns.Add("Transaction Type", typeof(string));
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
                    dr["Transaction Type"] = objResult.TransactionType;
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
                    worksheet.Cells[2, 11, dtOrderDetail.Rows.Count + 1, 17].Style.Numberformat.Format = "$#,##0.00";

                    FileInfo objFile = objTokenDetails.GetFileInfo(AppDomain.CurrentDomain.BaseDirectory, "OrderDetails_" + sheetName + "_" + DateTime.Now.Ticks.ToString() + ".xlsx");
                    package.SaveAs(objFile);
                }
            }
        }
    }
}
