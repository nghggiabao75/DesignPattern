namespace PatternApplication.FactoryMethodPattern
{
    public interface INotification
    {
        void Send(string message);
    }

    // Concrete products
    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Send email: {message}");
        }
    }

    public class SMSNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Send SMS: {message}");
        }
    }

    // Creator
    public interface INotificationFactory
    {
        public INotification CreateNotification();
    }

    // Concrete Factories
    public class EmailNotificationFactory : INotificationFactory
    {
        public INotification CreateNotification()
        {
            return new EmailNotification();
        }
    }

    public class SMSNotificationFactory : INotificationFactory
    {
        public INotification CreateNotification()
        {
            return new SMSNotification();
        }
    }


    // Client
    public class NotificationService
    {
        private readonly INotificationFactory _factory;

        public NotificationService(INotificationFactory factory)
        {
            _factory = factory;
        }

        public void NotifyUser(string message)
        {
            var notification = _factory.CreateNotification();
            notification.Send(message);
        }
    }
}
