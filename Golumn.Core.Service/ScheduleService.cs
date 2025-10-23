using Quanta.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quanta.Core.Service
{
    public class ScheduleService : BaseService
    {
        public List<string> GetSprintNames(List<SprintSchedule> schedules)
        {
            var sprintNames = new List<string>();
            foreach (var schedule in schedules)
            {
                sprintNames.Add(schedule.Name);
            }
            return sprintNames;
        }

        public List<SprintSchedule> GetSprintsFromJson(string filePath)
        {
            try
            {
                if (CreateIfDoesNotExist(filePath))
                {
                    var dummySchedules = new List<SprintSchedule>
                        {
                            new SprintSchedule { Id = 1, Name = "Dummy Sprint", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) }
                        };
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(dummySchedules));
                }

                var json = File.ReadAllText(filePath);
                var schedules = JsonConvert.DeserializeObject<List<SprintSchedule>>(json) ?? new List<SprintSchedule>();
                return schedules;
            }
            catch (Exception ex)
            {
                return new List<SprintSchedule>();
            }
        }
    }
}
