namespace IntegrationServices
{
    using IntegrationServices.RabbitMQServices;
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MQSubscriberService : BackgroundService
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var messageParts = MessageDecoder.MessageParts(message);

                var reqBody = new NewsDTO
                {
                    DateCreated = DateTime.Now,
                    Title = MessageDecoder.MessageTitle(messageParts),
                    Text = MessageDecoder.MessageText(messageParts),
                    Image = MessageDecoder.MessageImageExtension(messageParts) + ";" + MessageDecoder.MessageImageData(messageParts),
                };
                var json = JsonSerializer.Serialize(reqBody);

                PostMessageToAPI.PostMessage(json);
            };

            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);


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
