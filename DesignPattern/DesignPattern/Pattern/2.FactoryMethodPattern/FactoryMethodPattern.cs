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
        INotification CreateNotification();
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

    // Factory Provider Registry
    public interface INotificationFactoryProvider
    {
        INotificationFactory GetFactory(string type);
    }

    public class NotificationFactoryProvider : INotificationFactoryProvider
    {
        private readonly IDictionary<string, INotificationFactory> _factories;

        public NotificationFactoryProvider(IDictionary<string, INotificationFactory> factories)
        {
            _factories = factories;
        }

        public INotificationFactory GetFactory(string type)
        {
            if (_factories.TryGetValue(type, out var factory))
                return factory;

            throw new ArgumentException($"Unsupported notification type: {type}");
        }
    }

    // Client
    public class NotificationService
    {
        private readonly INotificationFactoryProvider _factoryProvider;

        public NotificationService(INotificationFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
        }

        public void NotifyUser(string message, string type)
        {
            var factory = _factoryProvider.GetFactory(type);
            var notification = factory.CreateNotification();
            notification.Send(message);
        }
    }
}
