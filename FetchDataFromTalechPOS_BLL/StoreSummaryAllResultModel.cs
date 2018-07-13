﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{

    public class SummaryResultModel
    {

        //public int cost { get; set; }
        public double discount { get; set; }
        //public int employeeGratuity { get; set; }
        public int gratuity { get; set; }
        public double grossRevenue { get; set; }
        //public int houseAccountBalance { get; set; }
        //public int loyalty { get; set; }
        public int netGiftCardsRevenue { get; set; }
        //public int netGiftCardsSold { get; set; }
        public double netRevenue { get; set; }
        public double netRevenuePerReceipt { get; set; }
        public double noneTaxableRevenue { get; set; }
        public double profitability { get; set; }
        public int receipts { get; set; }
        public double refundAmount { get; set; }
        //public int refundDiscountAmount { get; set; }
        //public int refundGratuityAmount { get; set; }
        public int refundReceipts { get; set; }
        //public int refundTaxAmount { get; set; }
        //public double reportedTax { get; set; }
        //public int roundingRevenue { get; set; }
        //public int storeCreditBalance { get; set; }
        //public int storeCreditGranted { get; set; }
        //public int storeCreditOnlyBalance { get; set; }
        //public int storeCreditReceipts { get; set; }
        //public double subTotal { get; set; }
        public double tax { get; set; }
        //public double taxableRevenue { get; set; }
        public double tip { get; set; }
        public double totalCollected { get; set; }
        //public int totalCustomers { get; set; }

        public string storeName { get; set; }

    }


}
