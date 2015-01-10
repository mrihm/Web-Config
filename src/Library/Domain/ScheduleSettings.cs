using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Config.Library.Domain
{
    /// <summary>
    /// Schedule class that encapsulates the notification schedule information
    /// </summary>
    public class Schedule
    {
        private int _startDelay;

        /// <summary>
        /// The number of hours and minutes after midnight when the service should run
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Set and return the flag that indicates whether to run the process immediately or whether to schedule the execution
        /// </summary>
        public bool StartImmediately { get; set; }

        /// <summary>
        /// Set and return the delay, in milliseconds, if the process is to be run immediately
        /// </summary>
        public int StartDelay
        {
            get { return (this.StartImmediately) ? _startDelay : 0; }
            set
            {
                if (value < 0) {
                    throw new ArgumentException("Invalid start delay value");
                }

                _startDelay = value;
            }
        }
    }

    /// <summary>
    /// Domain model class that encapsulates the scheduling information
    /// </summary>
    public class Timetable
    {
        /// <summary>
        /// Get and set the interval until a service should run next
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Get and set the optional start date for the service 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Get and set the optional end date for the service 
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
