using FetchDataFromTalechPOS_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OrderDetailsSampleService
{
    public partial class OrderDetailsSampleService : ServiceBase
    {
        Timer _timer = new System.Timers.Timer();
        public OrderDetailsSampleService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            WindowsServiceSchedler(_timer);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                #region Process ProcessFeefoResults

                _timer.Stop();
                ExecuteTask();
                WindowsServiceSchedler(_timer);

                #endregion
            }

            catch (Exception)
            {
                _timer.Start();

            }

        }

        public void ExecuteTask()
        {
            TalechAPIMethods objMethod = new TalechAPIMethods();
            objMethod.RunAsync().GetAwaiter().GetResult();

            LogHelper.Log("Execute Task completed. " + DateTime.Now);
            //LogHelper.Log("Execute Task method called");
        }

        private static void WindowsServiceSchedler(System.Timers.Timer _timer)
        {
            string _runweekly = Convert.ToString(ConfigurationManager.AppSettings["Weekly"]);
            string _weeklyeventTriggerTime = Convert.ToString(ConfigurationManager.AppSettings["WeeklyeventTriggerTime"]);
            string _dayOfWeek = Convert.ToString(ConfigurationManager.AppSettings["DayOfWeek"]);
            DayOfWeek MyDays = (DayOfWeek)DayOfWeek.Parse(typeof(DayOfWeek), _dayOfWeek);
            string _DailyEventTriggerTime = Convert.ToString(ConfigurationManager.AppSettings["DailyEventTriggerTime"]);
            Scheduler sch = new Scheduler("OrderDetailsSampleService");
            if (_runweekly == "true")
            {

                sch.ScheduleWeekly(MyDays, _weeklyeventTriggerTime, _timer);
            }
            else
            {
                LogHelper.Log("Service executed on daily. Time:" + _DailyEventTriggerTime);
                sch.ScheduleDaily(_DailyEventTriggerTime, _timer);
            }
        }

        protected override void OnStop()
        {
            _timer.Stop();
            LogHelper.Log("service stopped");
        }
    }
}
