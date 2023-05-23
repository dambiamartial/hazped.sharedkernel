namespace hazped.sharedkernel.Messaging;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent, INotification { }