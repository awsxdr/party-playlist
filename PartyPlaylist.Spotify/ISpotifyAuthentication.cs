namespace PartyPlaylist.Spotify
{
    using Func;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public interface ISpotifyAuthentication
    {
        Result<AccessTokens> GetLoginDetails();
        Result<bool> IsLoggedIn();
        Task<Result> SetAuthorizationCode(AuthorizationCode code);
        Result<AuthenticationHeaderValue> GetAuthenticationHeader();
    }
}