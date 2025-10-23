using System;

namespace Quanta.Core.Domain
{
    public class LogTask
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Constructor to initialize a new LogTasks instance.
        /// </summary>
        public LogTask()
        {
        }

        public string Project { get; set; }

        public void MarkComplete()
        {
            IsComplete = true;
        }

        public override string ToString()
        {
            return $"[IsComplete: {IsComplete}, Description: {Description}, CreatedDate: {CreatedDate}]";
        }
    }
}
