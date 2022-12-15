namespace IntegrationAPI.Token
{
    public interface ITokenHelper
    {
        string GenerateToken(int id, string email);
    }
}
