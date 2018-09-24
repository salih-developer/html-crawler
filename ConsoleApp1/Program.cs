using System;

namespace ConsoleApp1
{
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using Quartz;
    using Quartz.Impl;
    class Program
    {


        private static IScheduler _scheduler;
        private static void Main(string[] args)
        {
            InitializeJobs();
            Console.WriteLine("Zamanlanmış görevler başladı!");
            Console.ReadLine();
        }
        private static async void InitializeJobs()
        {
            _scheduler = await new StdSchedulerFactory().GetScheduler();
            await _scheduler.Start();
            var userEmailsJob = JobBuilder.Create<SendUserEmailsJob>().WithIdentity("SendUserEmailsJob").Build();
            var userEmailsTrigger = TriggerBuilder.Create().WithIdentity("SendUserEmailsCron").StartNow().WithCronSchedule("0 */30 * ? * *").Build();
            var result = await _scheduler.ScheduleJob(userEmailsJob, userEmailsTrigger);

        }
        public class SendUserEmailsJob : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                _scheduler.PauseAll();
                ScrapingBusiness business = new ScrapingBusiness();
                business.Start();
                _scheduler.ResumeAll();
                return Task.CompletedTask;
            }
        }
    }
}
