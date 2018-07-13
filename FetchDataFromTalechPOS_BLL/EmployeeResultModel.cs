using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{

    public class EmployeeResultModel
    {
        public string email { get; set; }
        public string employeeNumber { get; set; }
        public bool hasMultipleStoreAccess { get; set; }
        public int id { get; set; }
        public int isActive { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }
        public int storeId { get; set; }
        public bool superUser { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userName { get; set; }
        public string usrGroupCode { get; set; }
    }
}
