namespace IntegrationServices
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using IntegrationServices.RabbitMQServices;

    internal class MQSubscriberService: BackgroundService
    {
        IConnection connection;
        IModel channel;
        
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            // message catching
            var consumer = new EventingBasicConsumer(channel);  // consumer just consumes basic events
            consumer.Received += (model, ea) => // init what to do when event hits
            {
                var body = ea.Body.ToArray();   // body is byte[]
                var message = Encoding.UTF8.GetString(body);    // unpack body into string

                var messageParts = MessageDecoder.MessageParts(message);
                MessageDecoder.MessageImage(messageParts);
            };

            channel.BasicConsume(queue: "hello",    // consume from where
                                 autoAck: true,     // autoacknowledge
                                 consumer: consumer);   // who is consuming


            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Close();
            connection.Close();
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
