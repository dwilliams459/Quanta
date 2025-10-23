using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quanta.Core.Domain
{
    public class SprintSchedule
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectName { get; set; }

        public int DaysRemaining
        {
            get
            {
                int remaingDays = (EndDate - System.DateTime.Now).Days;
                return remaingDays;
            }
        }

        /// <summary>
        /// Returns true if the sprint is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (StartDate == null)
                {
                    return (EndDate - System.DateTime.Now).TotalDays <= 5;
                }

                return (StartDate <= System.DateTime.Today && EndDate >= System.DateTime.Today);
            }
        }
    }
}
