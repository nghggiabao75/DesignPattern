namespace PatternApplication.AbstractFactoryPattern
{
    // Product 1
    public interface INotification
    {
        void Send(string message);
    }

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

    // Product 2
    public interface IMessageFormatter
    {
        string Format(string message);
    }

    public class SimpleFormatter : IMessageFormatter
    {
        public string Format(string message)
        {
            return message;
        }
    }

    public class FancyFormatter : IMessageFormatter
    {
        public string Format(string message)
        {
            return $"*** {message.ToUpper()} ***";
        }
    }

    // Abstract Factory
    public interface INotificationFactory
    {
        INotification CreateNotification();
        IMessageFormatter CreateFormatter();
    }

    // Concrete Factories
    public class EmailNotificationFactory : INotificationFactory
    {
        public INotification CreateNotification()
        {
            return new EmailNotification();
        }

        public IMessageFormatter CreateFormatter()
        {
            return new SimpleFormatter();
        }
    }

    public class SMSNotificationFactory : INotificationFactory
    {
        public INotification CreateNotification()
        {
            return new SMSNotification();
        }

        public IMessageFormatter CreateFormatter()
        {
            return new FancyFormatter();
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
            var formatter = _factory.CreateFormatter();
            var formattedMessage = formatter.Format(message);

            var notification = _factory.CreateNotification();
            notification.Send(formattedMessage);
        }
    }
}
