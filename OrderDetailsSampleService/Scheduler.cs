using FetchDataFromTalechPOS_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OrderDetailsSampleService
{
    public class Scheduler
    {
        # region Fields
        /// <summary>
        /// This event is fired whenever the scheduling criteria is met.
        /// Run your code in the handler.
        /// </summary>     

        private ScheduleInterval _interval;
        private string _timeString;
        private DayOfWeek _dayOfWeek;
        private int _dayOfMonth;


        # endregion

        # region Constructors
        /// <summary>
        /// Makes it easy to schedule windows services
        /// </summary>
        /// <param name="serviceName">Name of the service. Without white spaces</param>
        public Scheduler(string serviceName)
        {
            //_timer = new System.Timers.Timer();
            //_timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);

        }
        # endregion


        private double GetNextInterval()
        {

            string[] arrTime = _timeString.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            int hours = Convert.ToInt32(arrTime[0]);
            int minutes = Convert.ToInt32(arrTime[1]);
            DateTime t = DateTime.Now.Date.AddHours(hours).AddMinutes(minutes);
            //
            TimeSpan ts = new TimeSpan();
            int x;

            switch (_interval)
            {
                case ScheduleInterval.EveryDay:

                    ts = t - System.DateTime.Now;
                    if (ts.TotalMilliseconds < 0)
                    {
                        ts = t.AddDays(1) - System.DateTime.Now;
                    }
                    LogHelper.Log("Next Service execution, after " + ts.Hours + " hours " + ts.Minutes + " Minutes " + ts.Seconds + " seconds");
                    break;

                case ScheduleInterval.EveryMonth:
                    int daysInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
                    if (System.DateTime.Now.Day > _dayOfMonth)
                    {
                        t = t.AddDays((daysInMonth - System.DateTime.Now.Day) + _dayOfMonth);
                    }
                    else if (_dayOfMonth == System.DateTime.Now.Day)
                    {
                        if (t < System.DateTime.Now)
                        {
                            t = t.AddDays(daysInMonth);
                        }
                    }
                    else
                    {
                        x = _dayOfMonth - System.DateTime.Now.Day;
                        t = t.AddDays(x);
                    }

                    ts = (TimeSpan)(t - System.DateTime.Now);
                    break;
                case ScheduleInterval.Every4Week:
                    t = t.AddDays(28);
                    ts = (TimeSpan)(t - System.DateTime.Now);

                    break;

                case ScheduleInterval.EveryWeek:
                    if (System.DateTime.Now.DayOfWeek > this._dayOfWeek)
                    {
                        x = System.DateTime.Now.DayOfWeek - this._dayOfWeek;
                        t = t.AddDays(7 - x);

                    }
                    else if (System.DateTime.Now.DayOfWeek == this._dayOfWeek)
                    {
                        if (t < System.DateTime.Now)
                        {
                            t = t.AddDays(7);
                        }
                    }
                    else
                    {
                        x = this._dayOfWeek - System.DateTime.Now.DayOfWeek;
                        t = t.AddDays(x);
                    }

                    ts = (TimeSpan)(t - System.DateTime.Now);
                    break;
            }

            return ts.TotalMilliseconds;
        }

        private void SetTimer(System.Timers.Timer _timer)
        {
            double inter = (double)GetNextInterval();

            _timer.Interval = inter;
            _timer.Start();
        }


        /// <summary>
        /// Schedules the service to run at a specified time, daily.
        /// </summary>
        /// <param name="time">Takes the format HH:MM:SS AM/PM. Ex: 9:30 AM</param>
        public void ScheduleDaily(string time, Timer _timer)
        {
            this._interval = ScheduleInterval.EveryDay;
            this._timeString = time;
            Validate();
            SetTimer(_timer);
        }


        /// <summary>
        /// Schedules the service to run on a specified day of the week, at a specified time
        /// </summary>
        /// <param name="dayOfWeek">System.DayOfWeek enumeration</param>
        /// <param name="time">Takes the format HH:MM:SS AM/PM. Ex: 9:30 AM</param>
        public void ScheduleWeekly(DayOfWeek dayOfWeek, string time, System.Timers.Timer _timer)
        {
            this._interval = ScheduleInterval.EveryWeek;
            this._dayOfWeek = dayOfWeek;
            this._timeString = time;
            Validate();

            SetTimer(_timer);
        }


        /// <summary>
        /// Schedules the service to run once a month on a specified day and specified time.
        /// </summary>
        /// <param name="dayOfMonth">Integer value between 1 and 31. Automatically adjusts for 30/28/29 days months</param>
        /// <param name="time">Takes the format HH:MM:SS AM/PM. Ex: 9:30 AM</param>
        public void ScheduleMonthly(int dayOfMonth, string time, System.Timers.Timer _timer)
        {
            int daysInMonth = DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month);
            if (_dayOfMonth > daysInMonth)
            { _dayOfMonth = daysInMonth; }

            this._interval = ScheduleInterval.EveryMonth;
            this._dayOfMonth = dayOfMonth;
            this._timeString = time;
            Validate();

            SetTimer(_timer);
        }

        public void Schedule4Weekly(string time, System.Timers.Timer _timer)
        {
            this._interval = ScheduleInterval.Every4Week;
            this._timeString = time;
            Validate();

            SetTimer(_timer);
        }


        private void Validate()
        {
            if (this._timeString == null || this._timeString.Trim() == "")
            {
                throw new ApplicationException("Time to fire cannot be null");
            }

        }

    }
}
