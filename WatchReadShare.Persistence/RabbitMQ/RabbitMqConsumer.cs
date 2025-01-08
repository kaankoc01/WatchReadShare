using RabbitMQ.Client;
using System.Text;
namespace WatchReadShare.Persistence.RabbitMQ
{
    public class RabbitMqConsumer(IConnectionFactory connectionFactory)
    {
        public void StartConsuming(string queueName)
        {
            var connection = connectionFactory.CreateConnectionAsync();
            var channel = connection.CreateModel();

            // Kuyruğu oluştur
            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mesaj alındı: {message}");
            };

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
            
        }
    }
}
