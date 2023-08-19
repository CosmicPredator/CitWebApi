namespace CitWebApi.Services;

public interface IAccessTokenValidator
{
    bool Validate(string accessToken);
}