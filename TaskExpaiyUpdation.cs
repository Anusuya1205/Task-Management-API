using Microsoft.Extensions.Hosting;
using Task_Management_API.BL;

namespace Task_Management_API

{
  
        public class TaskExpaiyUpdation : BackgroundService
        {
            private readonly BL_Task _blTask;

            public TaskExpaiyUpdation(BL_Task blTask)
            {
                _blTask = blTask;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _blTask.ExpireTasks();

                    await Task.Delay(60000, stoppingToken); 
                }

            }
        }
    }

