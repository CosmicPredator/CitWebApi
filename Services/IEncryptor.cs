namespace CitWebApi.Services;

public interface IEncryptor
{
    void generatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string generateAccessToken();
}