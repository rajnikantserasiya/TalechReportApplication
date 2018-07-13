using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public class TalechAccountDetails
    {
        public string refresh_token { get; set; }
        public string client_secret { get; set; }
        public string client_id { get; set; }
        public string grant_type { get; set; }
    }

    public class TalechSignOffUserDetails
    {
        public LoginUser user { get; set; }
        public int storeId { get; set; }
        public int merchantId { get; set; }
    }

    public class LoginUser
    {
        public int id { get; set; }
    }
}
