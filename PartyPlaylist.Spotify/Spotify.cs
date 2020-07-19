namespace PartyPlaylist.Spotify
{
    using Func;
    using System;
    using System.Threading.Tasks;
    using System.Web;

    using static Func.Option;

    public class Spotify : ISpotify
    {
        private readonly ISpotifyAuthentication _authentication;
        private readonly IHttpRequester _httpRequester;

        public Spotify(
            ISpotifyAuthentication authentication,
            IHttpRequester httpRequester)
        {
            _authentication = authentication;
            _httpRequester = httpRequester;
        }

        public Task<Result<SearchResult>> Search(string query) =>
            _authentication.GetAuthenticationHeader()
                .Then(x => _httpRequester.Get<SearchResult>(
                    new Uri($"https://api.spotify.com/v1/search?q={HttpUtility.UrlEncode(query)}&type=track"),
                    new HttpRequestInfo { Authentication = Some(x) }));
    }
}
