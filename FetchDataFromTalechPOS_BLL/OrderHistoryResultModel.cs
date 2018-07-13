using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class OrderHistoryResultModel
    {
        public string orderId { get; set; }
        public string transactionCode { get; set; }
        public double amount { get; set; }
        public double discount { get; set; }
        public string listOfItems { get; set; }
        public double revenue { get; set; }
        public double subTotal { get; set; }
        public double tip { get; set; }
        public double tax { get; set; }
        public string orderType { get; set; }
        public string paymentType { get; set; }
        public double refundAmount { get; set; }
        public double refundRevenue { get; set; }
        //public string Employee { get; set; }
        //public string storeName { get; set; }
        public string orderDate { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public List<PaymentInfo> paymentInfo { get; set; }
    }


    public class PaymentInfo
    {
        public string transactionType { get; set; }
    }

    public class OrderHistoryFullObject
    {
        //public SearchResult searchResult { get; set; }
        public ResponseCode ResponseCode { get; set; }
    }
}
