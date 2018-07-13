using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    class Program
    {       
        static void Main(string[] args)
        {
            TalechAPIMethods objMethod = new TalechAPIMethods();
            objMethod.RunAsync().GetAwaiter().GetResult();

            Console.ReadLine();
        }

       

    }

   
}
