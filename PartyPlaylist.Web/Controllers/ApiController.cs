namespace PartyPlaylist.Web.Controllers
{
    using Func;
    using Func.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using PartyPlaylist.Spotify;
    using PartyPlaylist.Spotify.Errors;
    using System.Linq;
    using System.Threading.Tasks;
    using static Func.Result;

    [Route("/api")]
    public class ApiController : ControllerBase
    {
        private readonly ISpotifyAuthentication _spotifyAuthentication;
        private readonly ISpotify _spotify;

        public ApiController(
            ISpotifyAuthentication spotifyAuthentication,
            ISpotify spotify)
        {
            _spotifyAuthentication = spotifyAuthentication;
            _spotify = spotify;
        }

        [HttpGet("login")]
        [OnFailure(typeof(NotLoggedInError), 404)]
        public Result GetLoggedInStatus() =>
            _spotifyAuthentication.IsLoggedIn().Then(x => x ? Succeed() : Fail(new NotLoggedInError()));

        [HttpGet("authcallback")]
        public async Task<IActionResult> AuthenticationCallback([FromQuery] string code) =>
            await _spotifyAuthentication.SetAuthorizationCode(code) switch
            {
                Success _ => Redirect("/"),
                Failure<HttpError> f => Redirect($"/error?code={f.Error.StatusCode}"),
                _ => Redirect("/error")
            };

        [HttpGet("search")]
        public async Task<Result<SearchResult>> Search([FromQuery] string query) =>
            await _spotify.Search(query);
    }
}
