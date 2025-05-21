namespace PatternApplication.SimpleFactoryPattern
{
    // Product interface
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

    // Simple Factory
    public class NotificationFactory
    {
        public INotification Create(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailNotification(),
                "sms" => new SMSNotification(),
                _ => throw new ArgumentException("Unsupported notification type", nameof(type))
            };
        }
    }

    // Client
    public class NotificationService
    {
        private readonly NotificationFactory _notificationFactory;

        public NotificationService(NotificationFactory notificationFactory)
        {
            _notificationFactory = notificationFactory;
        }

        public void NotifyUser(string type, string message)
        {
            // ❌ Bad approach: NotificationService is tightly coupled to EmailNotification
            // If you need to switch to SMS etc. later → YOU MUST MODIFY CODE HERE!
            //INotification notification = new EmailNotification();
            //notification.Send(message);

            // ✅ Better approach: use Simple Factory — separate object creation logic
            // Now, you only need to change logic inside NotificationFactory, no need to touch NotificationService
            INotification notification = _notificationFactory.Create(type);
            notification.Send(message);
        }
    }
}
