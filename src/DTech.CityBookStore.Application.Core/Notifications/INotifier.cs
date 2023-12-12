namespace DTech.CityBookStore.Application.Core.Notifications;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
    void Handle(string message);
}
