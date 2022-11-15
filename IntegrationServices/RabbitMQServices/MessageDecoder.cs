namespace IntegrationServices.RabbitMQServices
{
    using System;
    using System.IO;

    public class MessageDecoder
    {
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
            // do ovde ti efektivno treba

            string imagePath = Path.Combine(new string[] { projectDirectory, "recievedImage." + extension });
            var writer = new BinaryWriter(File.OpenWrite(imagePath));
            writer.Write(imageBytes);

            writer.Flush();
            writer.Dispose();
            writer.Close();
        }
    }
}
