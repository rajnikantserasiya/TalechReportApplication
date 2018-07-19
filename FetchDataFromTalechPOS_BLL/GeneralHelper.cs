using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public static class GeneralHelper
    {

        const double _3 = 0.001;
        const double _4 = 0.0001;
        const double _5 = 0.00001;
        const double _6 = 0.000001;
        const double _7 = 0.0000001;

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DateTime ResetTimeToStartOfDay(this DateTime dateTime)
        {
            return new DateTime(
               dateTime.Year,
               dateTime.Month,
               dateTime.Day,
               0, 0, 0, 0);
        }

        public static DateTime ResetTimeToEndOfDay(this DateTime dateTime)
        {
            return new DateTime(
               dateTime.Year,
               dateTime.Month,
               dateTime.Day,
               23, 59, 59, 999);
        }


        

        public static bool Equals3DigitPrecision(this double left, double right)
        {
            return Math.Abs(left - right) < _3;
        }

        public static bool Equals4DigitPrecision(this double left, double right)
        {
            return Math.Abs(left - right) < _4;
        }

    }
}
