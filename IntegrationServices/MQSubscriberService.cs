namespace IntegrationServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    internal class MQSubscriberService: BackgroundService
    {
        IConnection connection;
        IModel channel;

        public static String[] MessageParts(string message)
        {
            return message.Split(new string[] { '\n' + new String('-', 20) }, StringSplitOptions.None);
        }

        public static string MessageText(String[] messageParts)
        {
            return messageParts[messageParts.Length - 1];
        }

        public static string MessageTitle(String[] messageParts)
        {
            return messageParts[2];
        }

        public static void MessageImage(String[] messageParts)
        {
            // saves the image in the given format
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;

            string extension = messageParts[0];
            byte[] imageBytes = Convert.FromBase64String(messageParts[1]);

            string imagePath = Path.Combine(new string[] { projectDirectory, "recievedImage." + extension });
            var writer = new BinaryWriter(File.OpenWrite(imagePath));
            writer.Write(imageBytes);

            writer.Flush();
            writer.Dispose();
            writer.Close();
        }

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

                var messageParts = MessageParts(message);
                MessageImage(messageParts);
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
