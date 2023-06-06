using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Helpers;
using FarmFresh.Application.Interfaces.Handlers;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Infrastructure.Service.Services.Users;
using Microsoft.Extensions.DependencyInjection;

public class RemoteLocationHandler : IRemoteLocationHandler, IDisposable
{
    private static readonly object _lock = new object();
    private static bool _isProcessing = false;

    private readonly LocationQueueHelper _locationQueueHelper;
    private readonly IServiceProvider _serviceProvider;

    public RemoteLocationHandler(LocationQueueHelper locationQueueHelper, IServiceProvider serviceProvider)
    {
        _locationQueueHelper = locationQueueHelper;
        _serviceProvider = serviceProvider;
    }

    public async Task HandleStartProcessing()
    {
        if (!_isProcessing)
        {
            lock (_lock)
            {
                if (!_isProcessing)
                {
                    _isProcessing = true;
                }
                else
                {
                    return; // Another instance is already processing
                }
            }

            try
            {
                await ProcessLocationQueue();
            }
            finally
            {
                lock (_lock)
                {
                    _isProcessing = false;
                }
            }
        }
    }

    private async Task ProcessLocationQueue()
    {
        while (true) // Run indefinitely until queue is empty
        {
            LocationQueueRequest location;

            lock (_locationQueueHelper) // Lock the queue for thread safety
            {
                if (_locationQueueHelper.IsEmpty())
                {
                    break; // Exit the loop if the queue is empty
                }

                location = _locationQueueHelper.Dequeue();
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var locationService = scope.ServiceProvider.GetRequiredService<ILocationService>();
                await locationService.UpsertAsync(location, location.UserId);
            }
            
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    public void Dispose()
    {
        // No need to dispose of any resources
    }
}
