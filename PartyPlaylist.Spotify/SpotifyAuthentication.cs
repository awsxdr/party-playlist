namespace PartyPlaylist.Spotify
{
    using Func;
    using PartyPlaylist.Spotify.Errors;

    using static Func.Result;
    using static Func.Option;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;

    public class SpotifyAuthentication : ISpotifyAuthentication
    {
        private readonly IHttpRequester _httpRequester;
        private readonly AuthorizationConfiguration _authorizationConfiguration;
        private Option<AccessTokens> _accessTokens;

        public SpotifyAuthentication(IHttpRequester httpRequester, AuthorizationConfiguration authorizationConfiguration)
        {
            _httpRequester = httpRequester;
            _authorizationConfiguration = authorizationConfiguration;
        }

        public Result<AccessTokens> GetLoginDetails() =>
            _accessTokens switch
            {
                Some<AccessTokens> s => Succeed(s.Value),
                _ => Result<AccessTokens>.Fail(new NotLoggedInError())
            };

        public Result<bool> IsLoggedIn() => Succeed(_accessTokens is Some);

        public Task<Result> SetAuthorizationCode(AuthorizationCode code) =>
            GetAccessAndRefreshTokens(code)
                .Then(SetTokens);

        public Result<AuthenticationHeaderValue> GetAuthenticationHeader() =>
            GetLoginDetails()
            .Then(x => Succeed(new AuthenticationHeaderValue("Bearer", x.AccessToken)));

        private Task<Result<(string AccessToken, string RefreshToken)>> GetAccessAndRefreshTokens(AuthorizationCode code) =>
            _httpRequester.PostFormUrlEncoded<AccessAndRefreshTokenResponse>(new System.Uri("https://accounts.spotify.com/api/token"), 
                new (string, string)[] 
                {
                    ("grant_type", "authorization_code"),
                    ("code", code),
                    ("redirect_uri", "http://localhost:62974/api/authcallback")
                },
                new HttpRequestInfo
                {
                    Authentication = Some(new AuthenticationHeaderValue("Basic", _authorizationConfiguration.AuthorizationHeader))
                })
            .Then(x => Succeed((x.AccessToken, x.RefreshToken)));

        private Result SetTokens((string AccessToken, string RefreshToken) tokens)
        {
            _accessTokens = Some(new AccessTokens
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });

            return Succeed();
        }
    }
}
