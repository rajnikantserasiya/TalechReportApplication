using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class OrderHistoryInputParametersModel
    {
        public int offset { get; set; }
        public int limit { get; set; }
        //public string searchString { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        //public string[] paymentTypes { get; set; }
        public string[] orderStatus { get; set; }
        public string[] transactionTypes { get; set; }
        public int[] storeIds { get; set; }
        public string orderBy { get; set; }
        //public int userId { get; set; }
        public bool hideRefund { get; set; } // To display only sales transaction
        public bool hideSale { get; set; } // To display only refund transaction
        public bool onlySplitCheck { get; set; } // To display only split check transaction


    }

    public class OrderHistorySearchCriteria
    {
        public OrderHistoryInputParametersModel searchCriteria { get; set; }

    }

    public class EmployeeSearchCriteria
    {
        public EmpInputModel searchCriteria { get; set; }
    }

    public class EmpInputModel
    {
        public int activeFilter { get; set; }
    }

    public class OrderDetails
    {
        public string orderId { get; set; }
    }

    public class MenuItemsSearchCriteria
    {
        public int storeid { get; set; }
        public int offset { get; set; }
        public int maxRecords { get; set; }
        public string searchString { get; set; }
        public bool inventoryOnly { get; set; }
        public int categoryId { get; set; }
    }

    public class MenuUpdatesSearchCriteria
    {
        public int storeId { get; set; }
        public int offset { get; set; }
        public int maxRecords { get; set; }
    }

    public class AllMenuResultModel
    {

        public int categoryId { get; set; }
        public string categoryType { get; set; }
        public string name { get; set; }

    }
}
