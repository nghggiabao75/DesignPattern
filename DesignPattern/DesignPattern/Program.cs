using PatternApplication.FactoryMethodPattern;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddTransient<EmailNotificationFactory>();
builder.Services.AddTransient<SMSNotificationFactory>();

builder.Services.AddSingleton<IDictionary<string, INotificationFactory>>(sp =>
    new Dictionary<string, INotificationFactory>(StringComparer.OrdinalIgnoreCase)
    {
        ["Email"] = sp.GetRequiredService<EmailNotificationFactory>(),
        ["SMS"] = sp.GetRequiredService<SMSNotificationFactory>()
    });

builder.Services.AddSingleton<INotificationFactoryProvider, NotificationFactoryProvider>();
builder.Services.AddTransient<NotificationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();