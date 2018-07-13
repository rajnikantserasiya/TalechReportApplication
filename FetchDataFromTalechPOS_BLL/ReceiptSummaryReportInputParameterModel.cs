using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class ReceiptSummaryReportInputParameterModel
    {
        //public Int64 userID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public short granularity { get; set; }
        public bool includeShiftsData { get; set; }
        public int[] includedReports { get; set; }

    }

    public class ReportSearchCriteria
    {
        public ReceiptSummaryReportInputParameterModel searchCriteria { get; set; }
    }
}
