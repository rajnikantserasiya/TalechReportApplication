using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{

    public class OrderDetail
    {
        public double discountAmt { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double tax { get; set; }
        public int categoryId { get; set; }
        public int quantity { get; set; }
         public double refundedPrice { get; set; }
        public int refundedQuantity { get; set; }

    }

    public class PaymentDetail
    {
        public string cardType { get; set; }
        public string paymentType { get; set; }

    }

    public class Order
    {
        public string orderType { get; set; }
        public string orderCreationTime { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public List<PaymentDetail> paymentDetails { get; set; }
        public double tipsAmount { get; set; }
    }


    public class OrderDetailsResultModel
    {
        public Order Order { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string EmployeeName { get; set; }
        public string StoreName { get; set; }
    }

    public class OrderDetailsExportFields
    {
        public string TicketNo { get; set; }
        public string Type { get; set; }
        public string Employee { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Store { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string PaymentType { get; set; }
        public string TransactionType { get; set; }
        public string CreditType { get; set; }
        public double GrossSale { get; set; }
        public double Discounts { get; set; }
        public double Refunds { get; set; }
        public double NetSale { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public int QtySold { get; set; }
        public double Tips { get; set; }

    }


}
