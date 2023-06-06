using FarmFresh.Application.Dto.Request.Users;

namespace FarmFresh.Application.Helpers
{
    public sealed class LocationQueueHelper
    {
        private readonly Queue<LocationQueueRequest> locationQueue;
        
        public event Func<Task> StartProcessing;

        public LocationQueueHelper()
        {
            locationQueue = new Queue<LocationQueueRequest>();
        }
        
        public async Task Enqueue(LocationQueueRequest locationRequest)
        {
            locationQueue.Enqueue(locationRequest);
            await StartProcessing?.Invoke();
        }
        
        public LocationQueueRequest Dequeue()
        {
            return locationQueue.Dequeue();
        }

        public int Count()
        {
            return locationQueue.Count();
        }

        public bool IsEmpty()
        {
            return locationQueue.Count() == 0;
        }

        public void Clear()
        {
            locationQueue.Clear();
        }

        public LocationQueueRequest Peek()
        {
            return locationQueue.Peek();
        }

        public bool Contains(LocationQueueRequest locationRequest)
        {
            return locationQueue.Contains(locationRequest);
        }
    }
}
