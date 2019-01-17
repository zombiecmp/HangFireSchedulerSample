using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private const string ScheduleJobMessage = "Job was scheduled. It will run everyday at:";

        [Required]
        [DataType(DataType.Time)]
        [BindProperty]
        public DateTime Time { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
                       
        }

        public void OnPost()
        {
            var utcTime = Time.ToUniversalTime();
            RecurringJob.AddOrUpdate(() => ExecuteRecurrentJob(),
                Cron.Daily(utcTime.Hour,utcTime.Minute));
            Message = $"{ScheduleJobMessage} {Time.ToShortTimeString()}";
        }

        public void ExecuteRecurrentJob()
        {
            Console.WriteLine("Job Recurring every two minutes");
        }
    }
}
