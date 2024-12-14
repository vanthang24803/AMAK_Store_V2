using AMAK.Application.Providers.Tasks;
using Quartz;

namespace AMAK.API.Extensions {
    public static class TaskScheduling {
        public static void AddTaskScheduling(this IServiceCollection services) {
            services.AddQuartz(options => {
                var ticketJobKey = JobKey.Create(nameof(TicketJob));
                var flashSaleJobKey = JobKey.Create(nameof(FlashSaleJob));

                options.AddJob<TicketJob>(ticketJobKey)
                    .AddTrigger(trigger => trigger
                        .ForJob(ticketJobKey)
                       .WithCronSchedule(
                        "0 0 0 * * ?",
                        cronOptions => cronOptions
                            .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))
                ));

                options.AddJob<FlashSaleJob>(flashSaleJobKey)
                  .AddTrigger(trigger => trigger
                      .ForJob(flashSaleJobKey)
                     .WithCronSchedule(
                      "0 0 0 * * ?",
                      cronOptions => cronOptions
                          .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))
              ));

                // options.AddJob<FlashSaleJob>(flashSaleJobKey)
                // .AddTrigger(trigger => trigger.ForJob(flashSaleJobKey).WithSimpleSchedule(
                //     schedule => schedule.WithIntervalInMinutes(1).RepeatForever()
                // ));
            });

            services.AddQuartzHostedService(options => {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
