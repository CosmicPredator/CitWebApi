using CitWebApi.DB;

namespace CitWebApi.Services;

public class AccessTokenValidator : IAccessTokenValidator
{
    private readonly ApiContext Db;
    public AccessTokenValidator(ApiContext db) => Db = db;
    public bool Validate(string accessToken)
    {
        return !Db.Staffs.Any(x => x.AccessToken == accessToken);
    }
}