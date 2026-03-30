using Hangfire;
using MediatR;
using TaskScheduler.Application.Commands.CleanOldOrders;

namespace TaskScheduler.API.Jobs;

public static class RecurringJobsSetup
{
    public static void RegisterRecurringJobs(IServiceProvider serviceProvider)
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();

       
        RecurringJob.AddOrUpdate(
            "clean-old-orders",          
            () => mediator.Send(          
                new CleanOldOrdersCommand { DaysOld = 30 },
                CancellationToken.None),
            "0 2 * * *"                   
        );
    }
}
