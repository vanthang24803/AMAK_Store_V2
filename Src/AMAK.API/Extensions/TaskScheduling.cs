using AMAK.Application.Providers.Tasks;
using Quartz;

namespace AMAK.API.Extensions {
    public static class TaskScheduling {
        public static void AddTaskScheduling(this IServiceCollection services) {
            services.AddQuartz(options => {
                var ticketJobKey = JobKey.Create(nameof(TicketJob));

                options.AddJob<TicketJob>(ticketJobKey)
                    .AddTrigger(trigger => trigger
                        .ForJob(ticketJobKey)
                       .WithCronSchedule(
                        "0 0 1 * * ?",
                        cronOptions => cronOptions
                            .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))
                ));
            });

            services.AddQuartzHostedService(options => {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
