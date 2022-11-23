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

        public static string MessageImageExtension(String[] messageParts)
        {
            return messageParts[0];
        }

        public static string MessageImageData(String[] messageParts)
        {
            return messageParts[1];
        }

        public static string MessageTitle(String[] messageParts)
        {
            return messageParts[2];
        }

        public static string MessageText(String[] messageParts)
        {
            return messageParts[messageParts.Length - 1];
        }
    }
}
