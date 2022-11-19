namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer = null;
        public TimedHostedService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {

            using(var scope = _serviceProvider.CreateScope())
            {
                var _relocationService = scope.ServiceProvider.GetRequiredService<IRelocationService>();
                _relocationService.FinishRelocation();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
