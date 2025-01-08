using RabbitMQ.Client;
using System.Text;


namespace WatchReadShare.Persistence.RabbitMQ
{
    public class RabbitMqProducer(IConnectionFactory connectionFactory)
    {
        public void PublishMessage(string queueName, string message)
        {
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            // Kuyruğu oluştur
            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            // Mesajı kuyruğa gönder
            channel.BasicPublish(exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body);

            Console.WriteLine($"Mesaj gönderildi: {message}");
        }
    }
}