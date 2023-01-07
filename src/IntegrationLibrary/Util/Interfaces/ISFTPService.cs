namespace IntegrationLibrary.Util.Interfaces
{
    using System.IO;

    public interface ISFTPService
    {
        void SendFile(Stream fileStream);
    }
}
